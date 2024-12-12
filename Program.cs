using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;


namespace HomeWork_1
{
    internal class User
    {
        public string UserName { get; set; }
        private List<string> _tasks = new List<string>();
        public User(string defaultUserName)
        {
            UserName = defaultUserName;
        }

        public void AddTask(string task)
        {
            _tasks.Add(task);
        }

        public void RemoveTask(int number)
        {
            _tasks.RemoveAt(number);
        }

        public List<string> GetTasksList()
        {
            return _tasks;
        }
        public int GetTasksLenght()
        {
            return _tasks.Count;
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

                if (CheckInput(name))
                {
                    user.UserName = name;
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
            Print($"Я помогу тебе {user.UserName}", 1);
            Print("/start - эта команда позволит нам познакомиться, и я смогу называть Тебя по имени");
            Print("/info  - эта команда позволит Тебе узнать обо мне больше");
            Print("/echo  - эта команда введенная с аргументом, например /echo Hello, возвращает введенный текст, в данном примере \"Hello\"");
            Print("/addtask - эта команда позволит Тебе создать задание для меня");
            Print("/showtasks - эта команда позволит Тебе посмотреть список заданий для меня");
            Print("/removetask - эта команда удаляет задание из списка");
            Print("/exit  - этой командой мы попрощаемся и программа закроется");
        }

        public static void Info(User user)
        {
            Print($"{user.UserName}, я расскажу Тебе о себе", 1);
            Print("Я - программа имитации интерактивного бота \"Хоттабыч 1.0\", моя дата рождения 30.11.2024 г.");
        }
        public static void Echo(User user, string word)
        {
            if (word.Length >= 6)
            {
                word = word.Remove(0, 6);
                Print($"{user.UserName}, Твои слова эхом отразились в моём сердце и возвращаются к Тебе! {word}", 1);
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

        public static void AddTask(User user)
        {
            while (true)
            {
                Print($"{user.UserName}, я жду Твоих указаний! Напиши, что Ты хочешь.");
                string task = Console.ReadLine();

                if (CheckInput(task))
                {
                    user.AddTask(task);
                    Print("Я запомнил Твоё указание!");
                    break;
                }
                else
                {
                    Print("Ты так и не дал указание!");
                }
            }
        }

        public static void ShowTasks(User user)
        {
            int taskLenght = user.GetTasksLenght();
            if (taskLenght == 0)
            {
                Print($"{user.UserName}, список заданий пуст");
            }
            else
            {
                Print($"{user.UserName}, я помню Твои указания!");
                int number = 1;
                foreach (string task in user.GetTasksList())
                {
                    Print($"{number}. {task}");
                    number++;
                }
            }

        }

        public static void RemoveTask(User user)
        {
            ShowTasks(user);
            int taskLenght = user.GetTasksLenght();
            if (taskLenght == 0)
            {
                Print("Так что удалять нечего.");
            }
            else
            {
                while (true)
                {
                    Print("Какое указание Ты хочешь удалить? Введи его номер.");
                    string input = Console.ReadLine();
                    bool result = int.TryParse(input, out int taskNum);

                    if (taskNum > 0 && taskNum <= taskLenght)
                    {
                        user.RemoveTask(taskNum - 1);
                        Print($"Я удалил задание номер {taskNum}!");
                        break;
                    }
                    else
                    {
                        Print("Ты даешь неверный номер задания или это вообще не число! Попробуй еще раз.");
                    }
                }
            }
        }

        public static bool CheckInput(string input)
        {
            int charCounter = 0;
            foreach (char c in input)
            {
                if (c != ' ')
                {
                    charCounter++;
                }
            }
            if (charCounter > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            Print("/addtask");
            Print("/showtasks");
            Print("/removetask");
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
                    if (user.UserName == defaultUserName)
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
                else if (command.Contains("/addtask"))
                {
                    AddTask(user);
                }
                else if (command.Contains("/showtasks"))
                {
                    ShowTasks(user);
                }
                else if (command.Contains("/removetask"))
                {
                    RemoveTask(user);
                }
                else
                {
                    Print("Такой команды не существует!", 1);
                }
            }
        }
    }
}

