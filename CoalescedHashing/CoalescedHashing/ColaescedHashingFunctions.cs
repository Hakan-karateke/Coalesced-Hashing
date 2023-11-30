using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoalescedHashing
{
    internal class ColaescedHashingFunctions
    {
        public abstract class HashingTableBase
        {
            protected struct Data
            {
                public int KeyData;
                public int LinkAddress;

                public Data(int keyData)
                {
                    KeyData = keyData;
                    LinkAddress = -1;
                }
            }
            public int CakismaMiktarı;
            protected int mod;
            protected Data[] HashTable;
            public HashingTableBase(int modValue)
            {
                mod = modValue;
                HashTable = new Data[mod];
            }
            public int Hash(int key)
            {
                return key % mod;
            }
        }
        public class LISCH: HashingTableBase
        {
            public LISCH(int size) : base(size)
            {
                CakismaMiktarı=0;
                HashTable = new Data[size];
                mod = size;
            }
            public int FindEmpty(int key)
            {
                CakismaMiktarı++;
                int indexNumber=Hash(key);
                for (int i = 0; i < HashTable.Length; i++)
                {
                    if (HashTable[indexNumber].KeyData == 0) // 0 değeri bu slotun boş olduğunu gösterir
                        return indexNumber;
                    indexNumber++;
                    indexNumber=indexNumber%HashTable.Length;
                }
                return -1; // Tablo dolu
            }
            public void InsertLISCH(int key)
            {
                int index = Hash(key);
                if (HashTable[index].KeyData == 0) // Boş slot kontrolü
                {
                    HashTable[index] = new Data(key);
                }
                else
                {
                    int emptyIndex = FindEmpty(key);
                    if (emptyIndex == -1)
                    {
                        MessageBox.Show("Tablo dolu eleman eklenemiyor");
                    }
                    else
                    {
                        // Boş slot bulundu, zincirin sonuna ekle
                        int currentIndex = index;
                        while (HashTable[currentIndex].LinkAddress != -1)
                        {
                            currentIndex = HashTable[currentIndex].LinkAddress;
                        }
                        HashTable[currentIndex].LinkAddress = emptyIndex;
                        HashTable[emptyIndex] = new Data(key);
                    }
                }
            }
        }
        public class LICH: HashingTableBase
        {
            public int emptyAdress;
            public LICH(int size) : base(size)
            {
                CakismaMiktarı=0;
                HashTable = new Data[size];
                emptyAdress = (size - 1);
                mod = Convert.ToInt32(size * 0.8);
            }

            public int FindEmpty()
            {
                CakismaMiktarı++;
                for (int i = emptyAdress; i >= 0; i--)
                {
                    if (HashTable[i].KeyData == 0) // 0 değeri bu slotun boş olduğunu gösterir
                        return i;
                }
                return -1; // Tablo dolu
            }
            public void InsertLICH(int key)
            {
                int index = Hash(key);
                if (HashTable[index].KeyData == 0) // Boş slot kontrolü
                {
                    HashTable[index] = new Data(key);
                }
                else
                {
                    emptyAdress = FindEmpty();
                    if (emptyAdress == -1)
                    {
                        MessageBox.Show("Tablo dolu eleman eklenemiyor");
                    }
                    else
                    {
                        // Boş slot bulundu, zincirin sonuna ekle
                        int currentIndex = index;
                        while (HashTable[currentIndex].LinkAddress != -1)
                        {
                            currentIndex = HashTable[currentIndex].LinkAddress;
                        }
                        HashTable[currentIndex].LinkAddress = emptyAdress;
                        HashTable[emptyAdress] = new Data(key);
                    }
                }
            }
        }
        public class EICH: LICH
        {
            public EICH(int size) : base(size)
            {

            }
            public void InsertEICH(int key)
            {
                int index = Hash(key);
                if (HashTable[index].KeyData == 0) // Boş slot kontrolü
                {
                    HashTable[index] = new Data(key);
                }
                else
                {
                    emptyAdress = FindEmpty();
                    if (emptyAdress == -1)
                    {
                        MessageBox.Show("Tablo dolu eleman eklenemiyor");
                    }
                    else
                    {
                        // Boş slot bulundu, zincirin başından sonra ekle ekle
                        HashTable[emptyAdress] = new Data(key);
                        HashTable[emptyAdress].LinkAddress= HashTable[index].LinkAddress;
                        HashTable[index].LinkAddress = emptyAdress;
                    }
                }
            }
        }
        public class EISCH: HashingTableBase
        {
            public EISCH(int size) : base(size)
            {
                CakismaMiktarı=0;
                HashTable = new Data[size];
                mod = size;
            }

            public int FindEmpty(int key)
            {
                CakismaMiktarı++;
                int indexNumber = Hash(key);
                for (int i = 0; i < HashTable.Length; i++)
                {
                    if (HashTable[indexNumber].KeyData == 0) // 0 değeri bu slotun boş olduğunu gösterir
                        return indexNumber;
                    indexNumber++;
                    indexNumber = indexNumber % HashTable.Length;
                }
                return -1; // Tablo dolu
            }
            public void InsertEISCH(int key)
            {
                int index = Hash(key);
                if (HashTable[index].KeyData == 0) // Boş slot kontrolü
                {
                    HashTable[index] = new Data(key);
                }
                else
                {
                    int emptyIndex = FindEmpty(key);
                    if (emptyIndex == -1)
                    {
                        MessageBox.Show("Tablo dolu, eleman eklenemiyor.");
                    }
                    else
                    {
                        // Boş slot bulundu, zincire ekle
                        int currentIndex = index;

                        HashTable[emptyIndex] = new Data(key);
                        HashTable[emptyIndex].LinkAddress = HashTable[index].LinkAddress;
                        HashTable[index].LinkAddress = emptyIndex;
                    }
                }
            }
        }
        public class REISCH: HashingTableBase
        {
            public REISCH(int size) : base(size)
            {
                CakismaMiktarı = 0;
                HashTable = new Data[size];
                mod = size;
            }
            public int FindEmpty()
            {
                CakismaMiktarı++;
                List<int> emptyIndexes = new List<int>();
                // Boş olan indeksleri bul
                for (int i = 0; i < HashTable.Length; i++)
                {
                    if (HashTable[i].KeyData == 0) // 0 değeri bu slotun boş olduğunu gösterir
                    {
                        emptyIndexes.Add(i);
                    }
                }

                if (emptyIndexes.Count == 0)
                {
                    return -1; // Tablo dolu
                }
                // Boş indeksler arasından rastgele birini seç
                Random random = new Random();
                int randomIndex = emptyIndexes[random.Next(emptyIndexes.Count)];

                return randomIndex;
            }
            public void InsertREISCH(int key)
            {
                int index = Hash(key);
                if (HashTable[index].KeyData == 0) // Boş slot kontrolü
                {
                    HashTable[index] = new Data(key);
                }
                else
                {
                    int emptyIndex = FindEmpty();
                    if (emptyIndex == -1)
                    {
                        MessageBox.Show("Tablo dolu, eleman eklenemiyor.");
                    }
                    else
                    {
                        // Boş slot bulundu, zincire ekle
                        int currentIndex = index;

                        HashTable[emptyIndex] = new Data(key);
                        HashTable[emptyIndex].LinkAddress = HashTable[index].LinkAddress;
                        HashTable[index].LinkAddress = emptyIndex;
                    }
                }
            }
        }
        public class RLISCH: REISCH
        {
            public RLISCH(int size) : base(size)
            {
            }

            public void InsertRLISCH(int key)
            {
                int index = Hash(key);
                if (HashTable[index].KeyData == 0) // Boş slot kontrolü
                {
                    HashTable[index] = new Data(key);
                }
                else
                {
                    int emptyAdress = FindEmpty();
                    if (emptyAdress == -1)
                    {
                        throw new InvalidOperationException("Tablo dolu, eleman eklenemiyor.");
                    }
                    else
                    {
                        // Boş slot bulundu, zincirin sonuna ekle
                        int currentIndex = index;
                        while (HashTable[currentIndex].LinkAddress != -1)
                        {
                            currentIndex = HashTable[currentIndex].LinkAddress;
                        }
                        HashTable[currentIndex].LinkAddress = emptyAdress;
                        HashTable[emptyAdress] = new Data(key);
                    }
                }
            }
        }
        public class BLISCH: HashingTableBase
        {
            public static bool Isup;
            public BLISCH(int size) : base(size)
            {
                CakismaMiktarı = 0;
                Isup = true;
                HashTable = new Data[size];
                mod = size;
            }
            public int FindEmpty()
            {
                CakismaMiktarı++;
                if (Isup)
                {
                    for (int i = 0; i < HashTable.Length; i++)
                    {
                        if (HashTable[i].KeyData == 0) // 0 değeri bu slotun boş olduğunu gösterir
                            return i;
                    }
                    return -1; // Tablo dolu
                }
                else
                {
                    for (int i = (HashTable.Length - 1); i >= 0; i--)
                    {
                        if (HashTable[i].KeyData == 0) // 0 değeri bu slotun boş olduğunu gösterir
                            return i;
                    }
                    return -1; // Tablo dolu
                }
            }
            public void InsertBLISCH(int key)
            {
                int index = Hash(key);
                if (HashTable[index].KeyData == 0) // Boş slot kontrolü
                {
                    HashTable[index] = new Data(key);
                    Isup = !Isup;
                }
                else
                {
                    int emptyAdress = FindEmpty();
                    if (emptyAdress == -1)
                    {
                        MessageBox.Show("Tablo dolu, eleman eklenemiyor.");
                    }
                    else
                    {
                        // Boş slot bulundu, zincirin sonuna ekle
                        int currentIndex = index;
                        while (HashTable[currentIndex].LinkAddress != -1)
                        {
                            currentIndex = HashTable[currentIndex].LinkAddress;
                        }
                        HashTable[currentIndex].LinkAddress = emptyAdress;
                        HashTable[emptyAdress] = new Data(key);
                        Isup = !Isup;
                    }
                }
            }
        }
        public class BEISCH: BLISCH
        {
            public BEISCH(int size) : base(size)
            {

            }
            public void InsertBEISCH(int key)
            {
                int index = Hash(key);
                if (HashTable[index].KeyData == 0) // Boş slot kontrolü
                {
                    HashTable[index] = new Data(key);
                    Isup = !Isup;
                }
                else
                {
                    int emptyAdress = FindEmpty();
                    if (emptyAdress == -1)
                    {
                        MessageBox.Show("Tablo dolu, eleman eklenemiyor.");
                    }
                    else
                    {
                        // Boş slot bulundu, zincire ekle
                        int currentIndex = index;
                        HashTable[emptyAdress] = new Data(key);
                        HashTable[emptyAdress].LinkAddress = HashTable[index].LinkAddress;
                        HashTable[index].LinkAddress = emptyAdress;
                        Isup = !Isup;
                    }
                }
            }
        }

        public class AlgorithmResult
        {
            public string Algorithm { get; set; }
            public double InsertTime { get; set; }
            public int Cakisma { get; set; }
            public AlgorithmResult(string algorithm, double insertTime, int Cakisma)
            {
                this.Cakisma = Cakisma;
                this.Algorithm = algorithm;
                this.InsertTime = insertTime;
            }
        }

        public static List<AlgorithmResult> Results= new List<AlgorithmResult>();
        // Anahtarları ekleme ve arama işlemlerinin ortalama zamanını hesaplar
        public static void CalculateResults(int[] keys,
            ColaescedHashingFunctions.LISCH lischTable,ColaescedHashingFunctions.LICH lichTable, ColaescedHashingFunctions.EISCH eischTable,
            ColaescedHashingFunctions.REISCH reischTable, ColaescedHashingFunctions.RLISCH rlischTable,ColaescedHashingFunctions.BLISCH blischTable,
            ColaescedHashingFunctions.BEISCH beischTable,ColaescedHashingFunctions.EICH eichTable)
        {
            // Anahtarları ekme işlemi
            Stopwatch stopwatch = new Stopwatch();
            // LISCH
            stopwatch.Start();
            for (int i = 0; i < keys.Length; i++)
            {
                lischTable.InsertLISCH(keys[i]);
            }
            stopwatch.Stop();
            double InsertTime = stopwatch.ElapsedMilliseconds;
            Results.Add(new AlgorithmResult("LISCH", InsertTime,lischTable.CakismaMiktarı));
            // LICH
            stopwatch.Restart();
            for (int i = 0; i < keys.Length; i++)
            {
                lichTable.InsertLICH(keys[i]);
            }
            stopwatch.Stop();
            InsertTime = stopwatch.ElapsedMilliseconds;
            Results.Add(new AlgorithmResult("LICH", InsertTime, lichTable.CakismaMiktarı));
            // EICH
            stopwatch.Restart();
            for (int i = 0; i < keys.Length; i++)
            {
                eichTable.InsertEICH(keys[i]);
            }
            stopwatch.Stop();
            InsertTime = stopwatch.ElapsedMilliseconds;
            Results.Add(new AlgorithmResult("EICH", InsertTime, eichTable.CakismaMiktarı));
            // EISCH
            stopwatch.Restart();
            for (int i = 0; i < keys.Length; i++)
            {
                eischTable.InsertEISCH(keys[i]);
            }
            stopwatch.Stop();
            InsertTime = stopwatch.ElapsedMilliseconds;
            Results.Add(new AlgorithmResult("EISCH", InsertTime, eischTable.CakismaMiktarı));
            // REISCH
            stopwatch.Restart();
            for (int i = 0; i < keys.Length; i++)
            {
                reischTable.InsertREISCH(keys[i]);
            }
            stopwatch.Stop();
            InsertTime = stopwatch.ElapsedMilliseconds;
            Results.Add(new AlgorithmResult("REISCH", InsertTime, reischTable.CakismaMiktarı));
            // RLISCH
            stopwatch.Restart();
            for (int i = 0; i < keys.Length; i++)
            {
                rlischTable.InsertRLISCH(keys[i]);
            }
            stopwatch.Stop();
            InsertTime = stopwatch.ElapsedMilliseconds;
            Results.Add(new AlgorithmResult("RLISCH", InsertTime, rlischTable.CakismaMiktarı));
            // BLISCH
            stopwatch.Restart();
            for (int i = 0; i < keys.Length; i++)
            {
                blischTable.InsertBLISCH(keys[i]);
            }
            stopwatch.Stop();
            InsertTime = stopwatch.ElapsedMilliseconds;
            Results.Add(new AlgorithmResult("BLISCH", InsertTime, blischTable.CakismaMiktarı));
            // BEISCH
            stopwatch.Restart();
            for (int i = 0; i < keys.Length; i++)
            {
                beischTable.InsertBEISCH(keys[i]);
            }
            stopwatch.Stop();
            InsertTime = stopwatch.ElapsedMilliseconds;
            Results.Add(new AlgorithmResult("BEISCH", InsertTime, beischTable.CakismaMiktarı));
        }
    }
}
