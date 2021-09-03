using System;
using System.Numerics;


namespace RSA
{
	public class CryptRsa
	{
		public int p;
		public int q;
		public int n;
		private int f;
		public int d;
		public int e;

		public bool PrimeCheking(int num)
		{
			for (long i = 2; i <= (Math.Sqrt(num)); i++)
				if (num % i == 0)
					return false;
			return true;
		}

		private int MyltyplyN(int p, int k)
        {
			return p * k;
        }

		private int FEller(int p, int k)
        {
			return (p-1) * (k-1);
        }
		
		private int Euclid(int a, int b)
        {
			int r = 1;

			while (r != 0)
			{
				r = a - b * ((int)a/b);
				if (r == 0)
				{
					return b;
				}
				else
				{
					a = b;
					b = r;
				}
			}
			return b;
        }

		private int EuclidExtension(int a, int b)
        {
			int r = 1;
			int[,] Z = new int[2, 2] { { 0, 1 }, { 1,  -((int)(a / b))} };
			int[,] A = new int[2,2] { { 1, 0 }, { 0, 1 } };

			while(r!=0)
            {
				r = a - b * ((int)a / b);
				if (r==0)
                {
					if (A[1, 1] < 0)
					{
						A[1, 1] += f;
					}
					return A[1,1];
                }
                else
                {
					A = Multiplication(A, Z);
					a = b;
					b = r;
					Z[1, 1] = -(int)(a / b);
				}
            }

			if (A[1,1]<0)
            {
				A[1, 1] += f;
            }
			return A[1, 1];
        }

		private int[,] Multiplication(int[,] a, int[,] b)
		{
			int[,] r = new int[2, 2];
			for (int i = 0; i < b.GetLength(1); i++)
			{
				for (int j = 0; j < b.GetLength(0); j++)
				{
					r[i, j] = 0;
					for (int k = 0; k < b.GetLength(0); k++)
					{
						r[i, j] += a[i, k] * b[k, j];
					}
				}
			}
			return r;
		}

		public int[] EComputing()
        {
			int[] A= new int[1000];
           

			n=MyltyplyN(p,q);
			f = FEller(p, q);

            for (int i = 2, j=0; i < f; i++)
            {
				if (PrimeCheking(i) == true)
                {
					if (Euclid(f, i) == 1)
					{
						A[j] = i;
						j++;
					}
                }
            }
			return A;
        }

		public int DComputing()
        {
			d=EuclidExtension(f, e);
			return d;
		}

		private BigInteger Binpow(BigInteger a, BigInteger n)
		{
			if (n == 0)
				return 1;
			if (n % 2 == 1)
				return Binpow(a, n - 1) * a;
			else
			{
				BigInteger b = Binpow(a, n / 2);
				return BigInteger.Pow(b, 2);
			}
		}

		public char[] Crypting(char[] word)
        {
			BigInteger[] A= new BigInteger[100];

            for (int i = 0, j = 0; word[i] != '0'; i++,j++)
            {
                if (word[i]==' ')
                {
					A[j] = (BigInteger)word[i];
					i++;
					j++;
                }
				A[j] =(BigInteger)word[i] - 95;
            }

            for (int i = 0; A[i] != 0 ; i++)
            {
				A[i] = (Binpow(A[i], (BigInteger)e)) % (BigInteger)n;
			}

			for (int i = 0; A[i] != 0; i++)
			{
				word[i] = (char)(A[i]);
			}
			return word;
        }

		public char[] Decrypting(char[] word)
        {
			BigInteger[] A = new BigInteger[100];
			
			for (int i = 0, j = 0; word[i] != '0'; i++, j++)
			{
				A[j] = (BigInteger)word[i];
			}

			for (int i = 0; A[i] != 0; i++)
			{
				A[i] = (Binpow(A[i], (BigInteger)d)) % (BigInteger)n;
			}

			for (int i = 0; A[i] != 0; i++)
			{
				if (A[i] == 32)
				{
					word[i] = (char)(A[i]);
					i++;
				}
				word[i] = (char)(A[i] + 95);
			}
			return word;
		}
	}
}