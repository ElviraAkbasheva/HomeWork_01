using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Windows.Input;


namespace HomeWork_1
{
    internal class User
    {
        private string _userName;

        public User(string defaultUserName)
        {
            _userName = defaultUserName;
        }
        public void Set(string userName)
        {
            _userName = userName;
        }

        public string GetName()
        {
            return _userName;
        }
    }


    internal class Program
    {
        public static void Print(string text, int format = 0)
        {
            switch (format)
            {
                case 1:
                    Console.WriteLine($"\n{text}");
                    break;
                default:
                    Console.WriteLine($"{text}");
                    break;
            }
        }

        public static void Start(User user)
        {
            while (true)
            {
                Print("Введи своё имя", 1);
                string name = Console.ReadLine();
                int charCounter = 0;
                foreach (char c in name)
                {
                    if (c != ' ')
                    {
                        charCounter++;
                    }
                }
                if (charCounter > 0)
                {
                    user.Set(name);
                    Print($"Приятно познакомиться, {name}", 1);
                    Print("Теперь Тебе доступна команда \"/echo\"");
                    break;
                }
                else
                {
                    Print("Ты так и не представился!");
                }
            }
        }

        public static void Help(User user)
        {
            Print($"Я помогу тебе {user.GetName()}", 1);
            Print("/start - эта команда позволит нам познакомиться, и я смогу называть Тебя по имени");
            Print("/info  - эта команда позволит Тебе узнать обо мне больше");
            Print("/echo  - эта команда введенная с аргументом, например /echo Hello, возвращает введенный текст, в данном примере \"Hello\"");
            Print("/exit  - этой командой мы попрощаемся и программа закроется");
        }

        public static void Info(User user)
        {
            Print($"{user.GetName()}, я расскажу Тебе о себе", 1);
            Print("Я - программа имитации интерактивного бота \"Хоттабыч 1.0\", моя дата рождения 30.11.2024 г.");
        }
        public static void Echo(User user, string word)
        {
            if (word.Length >= 6)
            {
                word = word.Remove(0, 6);
                Print($"{user.GetName()}, Твои слова эхом отразились в моём сердце и возвращаются к Тебе! {word}", 1);
            }
            else
            {
                Print("Ты не ввёл аргумент у команды \"/echo\"!");
            }
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        static void Main()
        {
            string defaultUserName = "Незнакомец";
            User user = new User(defaultUserName);

            Print("Приветствую тебя, о Великий повелитель компьютера!");
            Print("Тебе доступны следующие команды:");
            Print("/start");
            Print("/help");
            Print("/info");
            Print("/exit");

            while (true)
            {
                Print(string.Empty);
                string command = Console.ReadLine();
                if (command.Contains("/start"))
                {
                    Start(user);
                }
                else if (command.Contains("/help"))
                {
                    Help(user);
                }
                else if (command.Contains("/info"))
                {
                    Info(user);
                }
                else if (command.Contains("/echo"))
                {
                    if (user.GetName() == defaultUserName)
                    {
                        Console.WriteLine($"Для выполнения команды \"/echo\", представься, {defaultUserName}, через команду \"/start\"");
                    }
                    else
                    {
                        Echo(user, command);
                    }
                }
                else if (command.Contains("/exit"))
                {
                    Exit();
                }
                else
                {
                    Print("Такой команды не существует!", 1);
                }
             }
        }
    }
}

