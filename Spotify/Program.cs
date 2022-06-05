using Reg_Log;
using WMPLib;
CreaPlayList play = new CreaPlayList();
Repro rep = new Repro();
char op, op1, op2;
int opInt, opInt1, auxint = 0;
string user, pssw, Ncancion, AUX;
#region reproductor
//Se realizó en el main porque no se puede llamar en las clases :D
void Reproductor(string URL)
{
    //Se realizó en el main porque no se puede llamar en las clases :D
    WindowsMediaPlayer Repro = new WindowsMediaPlayer();
    if (URL == Ncancion)
    {
        Repro.URL = $@"{Directory.GetCurrentDirectory()}\Música\{URL}.mp3";
        Repro.controls.play();
        do
        {
            Console.WriteLine("Reproduciendo:  {0}", URL.ToUpper());
            Console.WriteLine("\n\n\n\np = Pausa\t\t\tc = Continuar\t\t\ts = Salir");
            op1 = Convert.ToChar(Console.ReadLine().Substring(0, 1).ToLower());
            Console.Clear();
            switch (op1)
            {
                case 'p':
                    Repro.controls.pause();
                    break;
                case 'c':
                    Repro.controls.play();
                    break;
            }
        } while (op1 != 's');
        Repro.controls.stop();
    }
    else
    {
        Repro.URL = $@"{Directory.GetCurrentDirectory()}\Música\{URL}.mp3";
        Repro.controls.play();
        do
        {
            Console.WriteLine("No se encontró tu canción ahora se esta reproduciendo:  {0}", URL.ToUpper());
            Console.WriteLine("\n\n\n\np = Pausa\t\t\tc = Continuar\t\t\ts = Salir");
            op1 = Convert.ToChar(Console.ReadLine().Substring(0, 1).ToLower());
            Console.Clear();
            switch (op1)
            {
                case 'p':
                    Repro.controls.pause();
                    break;
                case 'c':
                    Repro.controls.play();
                    break;
            }
        } while (op1 != 's');
        Repro.controls.stop();
    }
}
void ReproductorPl()
{

    WindowsMediaPlayer Repro = new WindowsMediaPlayer();
    using (StreamReader can = new StreamReader($@"{Directory.GetCurrentDirectory()}\{user}.txt"))
    {
        int i;
        string data = can.ReadToEnd();
        string[] Ncan = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        for (i = 0; i < Ncan.Length - 1; i++)
        {
            Repro.URL = $@"{Directory.GetCurrentDirectory()}\Música\{Ncan[i]}.mp3";
            Repro.controls.play();
            do
            {
                Console.WriteLine("Reproduciendo:  {0}", Ncan[i].ToUpper());
                Console.WriteLine("\n\n\n\np = Pausa\t\t\tc = Continuar\t\t\tn = Siguiente Cancion\t\t\ts = Salir");
                op1 = Convert.ToChar(Console.ReadLine().Substring(0, 1).ToLower());
                Console.Clear();
                switch (op1)
                {
                    case 'p':
                        Repro.controls.pause();
                        break;
                    case 'c':
                        Repro.controls.play();
                        break;
                    case 'n':
                        Repro.controls.stop();
                        break;
                    case 's':

                        break;
                }
            } while (op1 != 'n' && op1 != 's');
            if (op1 == 's') { break; }
            Repro.controls.stop();
        }
        if (Ncan.Length - 1 == i) { Console.WriteLine("Fin de la PlayList"); Console.ReadKey(); Console.Clear(); }
    }
}
#endregion
#region menú
if (!RegLog.HasUser())
{
    do
    {
        Console.WriteLine("Bienvenido, no se encontraron usuarios");
        Console.WriteLine("Desea crear uno? s/n");
        op = Convert.ToChar(Console.ReadLine().ToLower().Substring(0, 1));
        Console.Clear();
        Console.Clear();
        if (op == 'n')
        {
            return;
        }
        else if (op == 's')
        {
            RegLog.createUser();
            Console.Clear();
        }
    } while (op != 'n' && op != 's');
}
do
{
    Console.WriteLine("Qué desea hacer?\n1.-Login\n2.-Registrarse\n3.-SALIR");
    opInt1 = Convert.ToInt16(Console.ReadLine());
    Console.Clear();
    switch (opInt1)
    {
        case 1:
            Console.Write("Ingrese su nickname: ");
            user = Console.ReadLine();
            Console.Write("Ingrese su contraseña: ");
            pssw = Console.ReadLine();
            Console.Clear();
            if (!RegLog.Verficar_Datos(user, pssw))
            {
                Console.WriteLine("\n\n\n\t\t\t\t\t\tAcceso Denegado");
                Console.WriteLine("\n\n\n\t\t\t\tPresione cualquier tecla para continuar");
                Console.ReadKey();
                Console.Clear();
            }
            else if (RegLog.Verficar_Datos(user, pssw))
            {
                Console.WriteLine("\t\t\t\t\t\tBienvenido {0}", user);

                do
                {
                    Console.WriteLine("¿Qué desea Hacer?\n1.-Buscar canción\n2.-Reproducir PlayList\n3.-Añadir canciones PlayList\n4.-SALIR");
                    opInt = Convert.ToInt16(Console.ReadLine());
                    Console.Clear();
                    switch (opInt)
                    {
                        case 1:
                            Console.Write("Ingrese el nombre de la canción: ");
                            Ncancion = Console.ReadLine().ToLower().Trim();
                            Console.Clear();
                            AUX = rep.EncotrarMusica(Ncancion);
                            Reproductor(AUX);
                            break;
                        case 2:
                            try
                            {
                                using (StreamReader can = new StreamReader($@"{Directory.GetCurrentDirectory()}\{user}.txt"))
                                {
                                    string data = can.ReadToEnd();
                                    string[] Ncan = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                                    if (Ncan.Length > 1)// ARREGLAR PRÓXIMAMENTE
                                    {
                                        ReproductorPl();
                                    }
                                    else
                                    {
                                        Console.WriteLine("No hay canciones en tu PlayList");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                }
                            }
                            catch (Exception ex) { Console.WriteLine("No hay una PlayList"); Console.ReadKey(); Console.Clear(); }
                            break;
                        case 3:
                            if (!File.Exists($@"{Directory.GetCurrentDirectory()}\{user}.txt"))
                            {
                                play.NuevaPL(user);
                            }
                            if (File.Exists($@"{Directory.GetCurrentDirectory()}\{user}.txt"))
                            {
                                do
                                {
                                    Console.WriteLine("¿Deseas Agregar una cancion? s/n");
                                    op = Convert.ToChar(Console.ReadLine().ToLower().Substring(0, 1));
                                    Console.Clear();
                                    if (op == 's')
                                    {
                                        Console.WriteLine("Escriba el nombre de la canción");
                                        Ncancion = Console.ReadLine().ToLower().Trim();
                                        AUX = rep.EncotrarMusica(Ncancion);
                                        Console.Clear();
                                        if (AUX == Ncancion)
                                        {
                                            Console.WriteLine("Se agregó correctamente su canción");
                                            play.NuevaPL(user, Ncancion);
                                            Thread.Sleep(2000);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.WriteLine("No encontramos su cancion, deseas agregar otra? s/n");
                                            op2 = Convert.ToChar(Console.ReadLine().ToLower().Substring(0, 1));
                                            Console.Clear();
                                            if (op2 == 's')
                                            {
                                                Console.WriteLine("Escriba el nombre de la canción");
                                                Ncancion = Console.ReadLine().ToLower();
                                                Console.Clear();
                                                if (rep.EncotrarMusica(Ncancion) == Ncancion)
                                                {
                                                    Console.WriteLine("Se agregó correctamente su canción");
                                                    play.NuevaPL(user, Ncancion);
                                                    Thread.Sleep(2000);
                                                    Console.Clear();
                                                }
                                                else { Console.WriteLine("No encontramos su canción"); }
                                            }
                                        }
                                    }
                                } while (op != 'n');
                            }
                            break;
                        case 4:

                            break;
                    }
                } while (opInt != 4);
            }
            break;
        case 2:
            RegLog.createUser();
            break;
        case 3:
            Console.WriteLine("\n\n\n\t\t\t\t\t\tGracias por preferirnos");
            Console.WriteLine("\n\n\n\t\t\t\t\tPresione cualquier tecla para SALIR");
            Console.ReadKey();
            Console.Clear();
            break;
        default:
            Console.WriteLine("Elijá una opción por favor"); Thread.Sleep(1000); Console.Clear();
            break;
    }
} while (opInt1 != 3);// aún no es definitivo
#endregion