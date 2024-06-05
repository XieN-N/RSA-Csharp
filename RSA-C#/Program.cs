using System.ComponentModel.Design.Serialization;
using System.Numerics;

start0:

Console.WriteLine("Вас приветствует алгоритм RSA-SHTUKA. Выберите необходимое действие:");
Console.WriteLine(" --- Создать открытый и закрытый ключи (Введите: 1)");
Console.WriteLine(" --- Зашифровать сообщение (Введите: 2)");
Console.WriteLine(" --- Расшифровать сообщение (Введите: 3)");

string chosen_number_s = Console.ReadLine();
int chosen_number = 0;
if (chosen_number_s != "")
{
    chosen_number = Convert.ToInt32(chosen_number_s);
}

else
{
    Console.WriteLine("Вы ничего не выбрали...");
}

if (chosen_number == 1)
{
    int codes = CreateCodes();
}
else if (chosen_number == 2)
{
    Console.WriteLine("Введите открытый ключ в формате: e n");
    string open_key = Console.ReadLine();
    if (open_key != "")
    {
        string[] open_key_ = open_key.Split(' ');
        int[] open_key_int = new int[open_key_.Length];
        for (int i = 0; i < open_key_.Length; i++)
        {
            open_key_int[i] = Convert.ToInt32(open_key_[i]);

        }
        int number_e = open_key_int[0];
        int number_n = open_key_int[1];
        Console.WriteLine("");

        string Encrypted = Encrypt(number_e, number_n);
    }
    else
    {
        Console.WriteLine("Вы ничего не ввели...");
    }
}
else if (chosen_number == 3)
{
    Console.WriteLine("Введите закрытый ключ в формате: d n");
    string closed_key = Console.ReadLine();
    if (closed_key != "")
    {
        string[] closed_key_ = closed_key.Split(' ');
        int[] closed_key_int = new int[closed_key_.Length];

        for (int i = 0; i < closed_key_.Length; i++)
        {
            closed_key_int[i] = Convert.ToInt32(closed_key_[i]);

        }
        int number_d = closed_key_int[0];
        int number_n = closed_key_int[1];
        Console.WriteLine("");
        int Decrypted = Decrypt(number_d, number_n);
        Console.WriteLine("");
    }
    else
    {
        Console.WriteLine("Вы ничего не ввели...");
    }
}

Console.WriteLine("Хотите запустить программу заново? (Если да - введите: 1)");
string zanovo_s = Console.ReadLine();
if (zanovo_s != "")
{
    int zanovo = Convert.ToInt32(zanovo_s);
    if (zanovo == 1)
    {
        goto start0;
    }
}

static int CreateCodes()
{
    static long CreateSimpleNumber()
    {
        Random rnd = new Random();
    start1:
        long p = rnd.Next(200, 400);
        for (int i = 2; i < p; i++)
        {
            if (p % i == 0)
            {
                goto start1;
            }
            else
            {
                //тогда всё гуд
            }
        }
        return p;
    }
    Console.WriteLine("Самостоятельно ввести p и q (В противном случае это сделает программа (предпочтительно))? (Если да - введите: 1)");
    string answer1_s = Console.ReadLine();
    int answer1 = 0;
    long p_final = 0;
    long q_final = 0;
    if (answer1_s != "")
    {
        answer1 = Convert.ToInt32(answer1_s);
    }
    else
    {
        answer1 = 0;
    }

    if (answer1 == 1)
    {
        Console.WriteLine("Введите простые p и q (1 не является простым) в формате: p q");
        Console.WriteLine("!!!ВНИМАНИЕ!!! выбирайте p и q такие, чтобы p*q >= 1105. В противном случае будут утеряны некоторые символы");
        string p_q_s = Console.ReadLine();
        if (p_q_s != "")
        {
            string[] p_q_ = p_q_s.Split(' ');
            int[] p_q_int = new int[p_q_.Length];

            for (int i = 0; i < p_q_.Length; i++)
            {
                p_q_int[i] = Convert.ToInt32(p_q_[i]);

            }
            p_final = p_q_int[0];
            q_final = p_q_int[1];

            if (p_final == 2)
            {
                if (p_final * q_final < 1105)
                {
                    Console.WriteLine("Вы ввели не простое число p или p*q < 1105. Программа сформирует p самостоятельно");
                    p_final = CreateSimpleNumber();
                }
            }
            else if (p_final <= 1)
            {
                Console.WriteLine("Вы ввели не простое число p или p*q < 1105. Программа сформирует p самостоятельно");
                p_final = CreateSimpleNumber();
            }
            else
            {
                for (int i = 2; i < p_final; i++)
                {
                    if (((p_final % i == 0) && p_final != 2) || p_final * q_final < 1105)
                    {
                        Console.WriteLine("Вы ввели не простое число p или p*q < 1105. Программа сформирует p самостоятельно");
                        p_final = CreateSimpleNumber();
                        break;
                    }
                    else
                    {
                        //тогда всё гуд
                    }
                }
            }

            if (q_final == 2)
            {
                if (p_final * q_final < 1105)
                {
                    Console.WriteLine("Вы ввели не простое число q или p*q < 1105. Программа сформирует q самостоятельно");
                    q_final = CreateSimpleNumber();
                    if ((q_final == p_final) || p_final * q_final < 1105)
                    {
                        while ((q_final == p_final) || p_final * q_final < 1105)
                        {
                            q_final = CreateSimpleNumber();
                        }
                    }
                }
            }
            else if (q_final <= 1)
            {
                Console.WriteLine("Вы ввели не простое число q или p*q < 1105. Программа сформирует q самостоятельно");
                q_final = CreateSimpleNumber();
                if ((q_final == p_final) || p_final * q_final < 1105)
                {
                    while ((q_final == p_final) || p_final * q_final < 1105)
                    {
                        q_final = CreateSimpleNumber();
                    }
                }
            }
            else
            {
                for (int i = 2; i < q_final; i++)
                {
                    if ((q_final % i == 0 && q_final != 2) || p_final * q_final < 1105)
                    {
                        Console.WriteLine("Вы ввели не простое число q или p*q < 1105. Программа сформирует q самостоятельно");
                        q_final = CreateSimpleNumber();
                        if ((q_final == p_final) || p_final * q_final < 1105)
                        {
                            while ((q_final == p_final) || p_final * q_final < 1105)
                            {
                                q_final = CreateSimpleNumber();
                            }
                        }
                        break;
                    }
                    else
                    {
                        //тогда всё гуд
                    }
                }
            }
            Console.WriteLine("");
        }
        else
        {
            Console.WriteLine("Вы ничего не ввели...");
        }
    }
    else
    {
        p_final = CreateSimpleNumber();
        q_final = CreateSimpleNumber();
        if (q_final == p_final)
        {
            while (q_final == p_final)
            {
                q_final = CreateSimpleNumber();
            }
        }
    }

    long n = p_final * q_final;
    long f = (p_final - 1) * (q_final - 1);

    static long CreateE(long f1)
    {
        Random rnd = new Random();
    start2:
        long e1 = rnd.Next(3, 1000);
        long f2 = f1;
        long e2 = e1;

        while (e2 != 0 && f2 != 0)
        {
            if (e2 > f2)
            {
                e2 = e2 % f2;
            }
            else
            {
                f2 = f2 % e2;
            }
        }

        long nodef = f2 + e2;

        for (int i = 2; i < e1; i++)
        {
            if (e1 % i == 0 || e1 >= f1 || nodef != 1)
            {
                goto start2;
            }
            else
            {
                //тогда всё гуд
            }
        }
        return e1;
    }

    long e = CreateE(f);

    static long CreateD(long f1, long e1)
    {
        Random rnd = new Random();
    start3:
        long d = rnd.Next(3, 300000);
        if ((e1 * d) % f1 != 1)
        {
            goto start3;
        }
        else
        {
            //тогда всё гуд
        }
        return d;
    }

    long d1 = CreateD(f, e);

    Console.WriteLine($"Открытый ключ: ({e}, {n})");
    Console.WriteLine($"Закрытый ключ: ({d1}, {n})");
    Console.WriteLine("");

    return 0;
}

static string Encrypt(int e, int n)
{
    Console.WriteLine("Введите сообщение: ");
    string message = Console.ReadLine(); //SSH{g00d_r3v3rs3_my_fr34nd}
    long[] message_ints = new long[message.Length];

    if (message != "")
    {
        for (int i = 0; i < message.Length; i++)
        {
            message_ints[i] = (long)(message[i]);
        }
    }
    for (int i = 0; i < message.Length; i++)
    {
        BigInteger c;
        c = BigInteger.Pow((BigInteger)message_ints[i], (int)e) % n;
        message_ints[i] = (int)c;
    }

    string zashifrovano = "";

    if (message != "")
    {
        zashifrovano = Convert.ToString(message_ints[0]);
    }
    else
    {
        Console.WriteLine("Вы ничего не ввели...");
    }

    for (int i = 1; i < message.Length; i++)
    {
        zashifrovano += (" " + Convert.ToString(message_ints[i]));
    }
    Console.WriteLine("");
    Console.WriteLine("Зашифрованное сообщение: ");
    Console.WriteLine(zashifrovano);
    Console.WriteLine("");

    return zashifrovano;
}
static int Decrypt(int d1, int n)
{
    Console.WriteLine("Введите зашифрованное сообщение:");
    string zashifrovano = Console.ReadLine();
    if (zashifrovano != "")
    {
        string[] string_ints = zashifrovano.Split(' ');
        int[] ints = new int[string_ints.Length];
        Console.WriteLine("");
        Console.WriteLine("Расшифрованное сообщение: ");
        for (int i = 0; i < string_ints.Length; i++)
        {
            ints[i] = Convert.ToInt32(string_ints[i]);
            BigInteger index;
            index = BigInteger.Pow(ints[i], (int)d1) % n;
            ints[i] = (int)index;
            Console.Write(Convert.ToChar(ints[i]));
        }
        Console.WriteLine("");
    }
    else
    {
        Console.WriteLine("Вы ничего не ввели...");
    }
    return 0;
}
