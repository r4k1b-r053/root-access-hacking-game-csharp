using System;

namespace HackingGame
{
    public interface IScorable
    {
        int CalculateScore(int attempts);
    }

    public abstract class HackChallenge : IScorable
    {
        public string ChallengeName { get; set; }
        public int DifficultyLevel { get; set; }
        public static int TotalChallengesCreated;

        public HackChallenge(string name, int difficulty)
        {
            ChallengeName = name;
            DifficultyLevel = difficulty;
            TotalChallengesCreated++;
        }

        public HackChallenge(HackChallenge other)
        {
            ChallengeName = other.ChallengeName;
            DifficultyLevel = other.DifficultyLevel;
            TotalChallengesCreated++;
        }

        public abstract void StartChallenge();

        public virtual void DisplayHint()
        {
            Console.WriteLine("Think like a hacker, not like a user.");
        }

        public virtual int CalculateScore(int attempts)
        {
            int baseScore = DifficultyLevel * 100;
            int penalty = attempts * 10;
            return Math.Max(0, baseScore - penalty);
        }

        public static bool operator >(HackChallenge a, HackChallenge b)
        {
            return a.DifficultyLevel > b.DifficultyLevel;
        }

        public static bool operator <(HackChallenge a, HackChallenge b)
        {
            return a.DifficultyLevel < b.DifficultyLevel;
        }
    }

    public class PasswordCracking : HackChallenge
    {
        public string TargetPassword { get; set; }

        public PasswordCracking(string name, int difficulty, string password)
            : base(name, difficulty)
        {
            TargetPassword = password;
        }

        public PasswordCracking(string password)
            : this("Password Cracking", 1, password)
        {
        }

        public PasswordCracking(PasswordCracking other)
            : base(other)
        {
            TargetPassword = other.TargetPassword;
        }

        public override void StartChallenge()
        {
            bool isSuccess = false;
            int attempts = 0;

            while (!isSuccess)
            {
                attempts++;
                Console.WriteLine($"Challenge: {ChallengeName}, Difficulty: {DifficultyLevel}");
                Console.WriteLine("Try to crack the password! (Hint: it's a 5-letter word)");

                DisplayHint();

                string userInput = Console.ReadLine();
                if (userInput == TargetPassword)
                {
                    int score = CalculateScore(attempts);
                    Console.WriteLine($"Password cracked! You've gained access! Score: {score}");
                    isSuccess = true;
                }
                else
                {
                    Console.WriteLine("Wrong password! Try again.");
                }
            }
        }

        public override void DisplayHint()
        {
            Console.WriteLine("Hint: The password can be typed using only the first five letters of the alphabet.");
        }

        public override int CalculateScore(int attempts)
        {
            int baseScore = DifficultyLevel * 120;
            int penalty = attempts * 15;
            return Math.Max(0, baseScore - penalty);
        }
    }

    public class FirewallBypass : HackChallenge
    {
        public string SecurityPhrase { get; set; }

        public FirewallBypass(string name, int difficulty, string phrase)
            : base(name, difficulty)
        {
            SecurityPhrase = phrase;
        }

        public FirewallBypass(FirewallBypass other)
            : base(other)
        {
            SecurityPhrase = other.SecurityPhrase;
        }

        public override void StartChallenge()
        {
            bool isSuccess = false;
            int attempts = 0;

            while (!isSuccess)
            {
                attempts++;
                Console.WriteLine($"Challenge: {ChallengeName}, Difficulty: {DifficultyLevel}");
                Console.WriteLine("Bypass the firewall by entering the correct security phrase.");

                DisplayHint();

                string userInput = Console.ReadLine();
                if (userInput == SecurityPhrase)
                {
                    int score = CalculateScore(attempts);
                    Console.WriteLine($"Firewall bypass successful! Score: {score}");
                    isSuccess = true;
                }
                else
                {
                    Console.WriteLine("Firewall blocked! Try again.");
                }
            }
        }

        public override void DisplayHint()
        {
            Console.WriteLine("Hint: The phrase contains both letters and numbers with mixed case.");
        }
    }

    public class SystemExploit : HackChallenge
    {
        public string ExploitCode { get; set; }

        public SystemExploit(string name, int difficulty, string exploitCode)
            : base(name, difficulty)
        {
            ExploitCode = exploitCode;
        }

        public SystemExploit(SystemExploit other)
            : base(other)
        {
            ExploitCode = other.ExploitCode;
        }

        public override void StartChallenge()
        {
            bool isSuccess = false;
            int attempts = 0;

            while (!isSuccess)
            {
                attempts++;
                Console.WriteLine($"Challenge: {ChallengeName}, Difficulty: {DifficultyLevel}");
                Console.WriteLine("You need to run a system exploit. Enter the correct exploit code.");

                DisplayHint();

                string userInput = Console.ReadLine();
                if (userInput == ExploitCode)
                {
                    int score = CalculateScore(attempts);
                    Console.WriteLine($"Exploit successful! You've gained root access. Score: {score}");
                    isSuccess = true;
                }
                else
                {
                    Console.WriteLine("Exploit failed! Try again.");
                }
            }
        }

        public override void DisplayHint()
        {
            Console.WriteLine("Hint: The exploit code is alphanumeric with a specific sequence.");
        }
    }

    class HackingGame
    {
        public int CurrentLevel { get; set; }
        public static int TotalGamesPlayed { get; private set; }

        public void StartGame()
        {
            StartGame(1);
        }

        public void StartGame(int startingLevel)
        {
            TotalGamesPlayed++;

            Console.WriteLine("Welcome to the Hacking Game!");
            Console.WriteLine("Your mission: Break into systems, bypass firewalls, and crack passwords.");
            CurrentLevel = startingLevel;

            while (CurrentLevel <= 3)
            {
                Console.WriteLine($"--- Level {CurrentLevel} ---");

                HackChallenge challenge = GenerateChallenge(CurrentLevel);

                challenge.StartChallenge();

                if (CurrentLevel == 3)
                {
                    Console.WriteLine("Congratulations! You've completed the game!");
                    break;
                }

                Console.WriteLine("Press any key to move to the next level.");
                Console.ReadKey();
                CurrentLevel++;
            }

            ShowGameStats();
        }

        public HackChallenge GenerateChallenge(int level)
        {
            switch (level)
            {
                case 1:
                    return new PasswordCracking("Password Cracking", 1, "abcde");
                case 2:
                    return new FirewallBypass("Firewall Bypass", 2, "Secure123");
                case 3:
                    return new SystemExploit("System Exploit", 3, "exploitcode123");
                default:
                    return null;
            }
        }

        public HackChallenge GenerateChallenge(int level, int customDifficulty)
        {
            HackChallenge challenge = GenerateChallenge(level);
            if (challenge != null)
            {
                challenge.DifficultyLevel = customDifficulty;
            }
            return challenge;
        }

        public static void ShowGameStats()
        {
            Console.WriteLine("=== GAME STATS ===");
            Console.WriteLine($"Total games played      : {TotalGamesPlayed}");
            Console.WriteLine($"Total challenges created: {HackChallenge.TotalChallengesCreated}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HackingGame game = new HackingGame();
            game.StartGame();
        }
    }
}

