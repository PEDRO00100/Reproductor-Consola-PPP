namespace Reg_Log
{
    class RegLog
    {
        readonly static string file = $@"{Directory.GetCurrentDirectory()}\userInformation.txt";
        public static bool HasUser()
        {
            if (File.Exists(file))
            {
                return true;

            }
            return false;
        }
        public static bool Verficar_Datos(string user, string pssw)
        {
            try
            {
                using (StreamReader la = new StreamReader(file))
                {
                    string data = la.ReadToEnd();
                    string[] UserF = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    if (UserF.Length <= 0) { return false; }
                    for (int i = 0; i < UserF.Length; i++)
                    {
                        if (UserF[i].Replace("user:", "").Equals(user) && UserF[i + 1].Replace("pass:", "").Equals(pssw)) { return true; }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            return false;
        }
        public static bool createUser()
        {
            Console.Write("Ingrese su nickname: ");
            string usu = Console.ReadLine();
            Console.Write("Ingrese su contraseña: ");
            string pssw = Console.ReadLine();
            Console.Clear();
            if (usu != string.Empty && pssw != string.Empty)
            {

                string inf = $"user:{usu}{Environment.NewLine}pass:{pssw}";
                return CreateFile(file, inf);

                return true;
            }
            return false;
        }
        public static bool CreateFile(string nomArch, string Text)
        {
            try
            {
                using (StreamWriter sp = new StreamWriter(nomArch, true))
                {
                    sp.WriteLine(Text);
                    sp.Close();
                }
                return true;
            }
            catch (IOException ex) { Console.WriteLine(ex); }
            return false;
        }
    }
    class CreaPlayList
    {
        public void NuevaPL(string usu)
        {
            using (StreamReader pl = new StreamReader($@"{Directory.GetCurrentDirectory()}\userInformation.txt"))
            {
                string data = pl.ReadToEnd();
                string[] UserF = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                if (UserF.Length <= 0) { Console.WriteLine("No se que pasó"); }
                for (int i = 0; i < UserF.Length; i++)
                {
                    if (UserF[i].Replace("user:", "").Equals(usu))
                    {
                        string PlayL = $@"{Directory.GetCurrentDirectory()}\{usu}.txt";
                        StreamWriter es = new StreamWriter(PlayL, true);
                        es.Close();
                    }
                }
            }
        }
        public void NuevaPL(string usu, string s)
        {
            using (StreamReader pl = new StreamReader($@"{Directory.GetCurrentDirectory()}\userInformation.txt"))
            {
                string data = pl.ReadToEnd();
                string[] UserF = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                if (UserF.Length <= 0) { Console.WriteLine("No se que pasó"); }
                for (int i = 0; i < UserF.Length; i++)
                {
                    if (UserF[i].Replace("user:", "").Equals(usu))
                    {
                        string PlayL = $@"{Directory.GetCurrentDirectory()}\{usu}.txt";
                        using (StreamWriter sp = new StreamWriter(PlayL, true))
                        {
                            sp.WriteLine(s);
                            sp.Close();
                        }
                    }
                }
            }
        }
    }
    class Repro
    {
        public string EncotrarMusica(string nomC)
        {
            using (StreamReader can = new StreamReader($@"{Directory.GetCurrentDirectory()}\canciones.txt"))
            {
                Random x = new Random();
                int r;
                string data = can.ReadToEnd();
                string[] Ncan = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                r = x.Next(Ncan.Length);
                if (Ncan.Length <= 0) { return ""; }
                for (int i = 0; i < Ncan.Length; i++)
                {
                    if (Ncan[i].Equals(nomC)) { return Ncan[i]; }
                }
                return Ncan[r];
            }
        }
    }
}