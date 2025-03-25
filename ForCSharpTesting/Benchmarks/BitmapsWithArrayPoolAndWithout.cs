using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;
using SkiaSharp;
using System.Runtime.InteropServices;

namespace ForCSharpTesting.Benchmarks;

[MemoryDiagnoser]
[InliningDiagnoser(true, true)]
[TailCallDiagnoser]
[EtwProfiler]
[ConcurrencyVisualizerProfiler]
[NativeMemoryProfiler]
//[ThreadingDiagnoser]
public class BitmapsWithArrayPoolAndWithout
{
    [Params(
        "bridge1.png",
        "bridge4.png"
    )]
    public string TestFile;

    public string Path = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\";

    public int[] Sizes = [320, 375, 425, 480, 640, 768, 960, 1024, 1280, 1366, 1440, 1600, 1920];

    public static void StartBenchmark()
    {
        BenchmarkRunner.Run<BitmapsWithArrayPoolAndWithout>();
    }

    [Benchmark]
    public async Task SkiaSharpAsStream()
    {
        // Загрузка файла в Stream
        using FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read);
        // Преобразование Stream в Image
        using var bitmap = SKBitmap.Decode(readerStream);
        for (int i = 0; i < Sizes.Length; i++)
        {
            var size = Sizes[i];
            if (size > bitmap.Width)
            {
                break;
            }

            using var resizedBitmap = bitmap.Resize(new SKImageInfo(size, size * bitmap.Height / bitmap.Width), default(SKSamplingOptions));
            // Сохранение в формате JPEG
            string outputFilePath = @$"{Path}\new\{TestFile}_SkiaSharpAsStream_image_{size}.jpg";
            using var image = SKImage.FromBitmap(resizedBitmap);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 80);

            using FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            using var dataStream = data.AsStream();
            await dataStream.CopyToAsync(writerStream);
        }
    }

    [Benchmark]
    public async Task SkiaSharpCustomStream()
    {
        // Загрузка файла в Stream
        using FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read);
        // Преобразование Stream в Image
        using var bitmap = SKBitmap.Decode(readerStream);
        for (int i = 0; i < Sizes.Length; i++)
        {
            var size = Sizes[i];
            if (size > bitmap.Width)
            {
                break;
            }

            using var resizedBitmap = bitmap.Resize(new SKImageInfo(size, size * bitmap.Height / bitmap.Width), default(SKSamplingOptions));
            // Сохранение в формате JPEG
            string outputFilePath = @$"{Path}\new\{TestFile}_SkiaSharpCustomStream_image_{size}.jpg";
            using var image = SKImage.FromBitmap(resizedBitmap);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 80);
            using var stream = new AdveSKDataReadOnlyStream(data);
            using FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            // Копирование данных из MemoryStream в FileStream
            await stream.CopyToAsync(writerStream);
        }
    }

    public class AdveSKDataReadOnlyStream : Stream
    {
        private SKData _data;

        public AdveSKDataReadOnlyStream(SKData data)
        {
            _data = data;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ValidateBufferArguments(buffer, offset, count);

            var span = _data.AsSpan();

            var bytesLeft = Length - Position;
            if (bytesLeft > count)
            {
                bytesLeft = count;
            }

            if (bytesLeft <= 0)
            {
                return 0;
            }

            int byteCount = (int)bytesLeft;
            while (--byteCount >= 0)
            {
                buffer[offset + byteCount] = span[(int)Position + byteCount];
            }

            Position += bytesLeft;

            return (int)bytesLeft;
        }

        public override int Read(Span<byte> buffer)
        {
            if (GetType() != typeof(MemoryStream))
            {
                return base.Read(buffer);
            }

            var bytesLeft = Math.Min(Length - Position, buffer.Length);
            if (bytesLeft <= 0)
            {
                return 0;
            }

            var span = _data.AsSpan();
            span.Slice((int)Position, (int)bytesLeft).CopyTo(buffer);

            Position += bytesLeft;

            return (int)bytesLeft;
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            ValidateBufferArguments(buffer, offset, count);

            // If cancellation was requested, bail early
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<int>(cancellationToken);
            }

            try
            {
                int result = Read(buffer, offset, count);
                return Task.FromResult(result);
            }
            catch (Exception exception)
            {
                return Task.FromException<int>(exception);
            }
        }

        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return ValueTask.FromCanceled<int>(cancellationToken);
            }

            try
            {
                return new ValueTask<int>(
                    MemoryMarshal.TryGetArray(buffer, out ArraySegment<byte> destinationArray) ?
                        Read(destinationArray.Array!, destinationArray.Offset, destinationArray.Count) :
                        Read(buffer.Span));
            }
            catch (Exception exception)
            {
                return ValueTask.FromException<int>(exception);
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead => _data is not null;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => _data.Size;
        public override long Position { get; set; } = 0;
    }

    //[Benchmark]
    public void SystemDrawing()
    {
        // Загрузка файла в Stream
        using FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read);
        // Преобразование Stream в Image
        using var image = System.Drawing.Image.FromStream(readerStream);
        for (int i = 0; i < Sizes.Length; i++)
        {
            var size = Sizes[i];
            if (size > image.Width)
            {
                break;
            }

            // Изменение размера изображения
            using var bitmap = new System.Drawing.Bitmap(image, new System.Drawing.Size(size, size * image.Height / image.Width));
            // Сохранение в формате JPEG
            string outputFilePath = @$"{Path}\new\{TestFile}_SystemDrawing_image_{size}.jpg";
            using MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.Position = 0;

            using FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            // Копирование данных из MemoryStream в FileStream
            memoryStream.CopyTo(writerStream);
        }
    }
}