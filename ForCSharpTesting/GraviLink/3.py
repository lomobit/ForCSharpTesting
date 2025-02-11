import math

#print(math.comb(n, k)) 

def main():
	n = 15
	p = 0.5
	print("n = " + str(n) + " | p = " + str(p))
	print()

	p_sum = 0
	for k in range(0, n, 1):
		comb = math.comb(n + k - 1, k)
		current = comb * p**(n + k - 1)
		p_sum += current

		print("k = " + str(k) + " => p(k) = " + str(current) + " | comb = " + str(comb))

	print()
	print("p_sum = " + str(p_sum))

if __name__ == "__main__":
	main()
