using System;
using System.IO;
using System.Media;
using System.Threading;
using Figgle; // Required for ASCII text rendering

class Program
{
    static void Main()
    {
        // Start the greeting audio in a separate thread
        Thread audioThread = new Thread(PlayGreeting);
        audioThread.Start();

        // Display the ASCII header while the audio plays
        DisplayAsciiHeader();

        // Wait for audio to finish before proceeding
        audioThread.Join();

        // Ask for user input
        Console.Write("What's your name? ");
        string name = Console.ReadLine();

        // Display personalized ASCII welcome message
        DisplayAsciiWelcomeMessage(name);

        // Start interaction loop
        StartChatbot();
    }

    static void PlayGreeting()
    {
        string folderPath = @"C:\Chatbot\Sounds";
        string audioFilePath = Path.Combine(folderPath, "greeting.wav");

        if (File.Exists(audioFilePath))
        {
            try
            {
                SoundPlayer player = new SoundPlayer(audioFilePath);
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing the greeting: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine($"Error: Audio file not found at {audioFilePath}");
        }
    }

    static void DisplayAsciiHeader()
    {
        Console.Clear();
        string folderPath = @"C:\Chatbot\Sounds";
        string asciiFilePath = Path.Combine(folderPath, "ascii_art.txt");

        // Set the text color to Blue for the ASCII header
        Console.ForegroundColor = ConsoleColor.Blue;

        if (File.Exists(asciiFilePath))
        {
            string asciiArt = File.ReadAllText(asciiFilePath);
            Console.WriteLine("==================================================");
            Console.WriteLine(asciiArt);
            Console.WriteLine("==================================================");
        }
        else
        {
            Console.WriteLine($"Error: ASCII art file not found at {asciiFilePath}");
        }

        // Reset the color to default after displaying the header
        Console.ResetColor();
    }

    static void DisplayAsciiWelcomeMessage(string name)
    {
        // Set the text color to Blue for the welcome message
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(Figgle.FiggleFonts.Standard.Render($"Welcome, {name}!"));
        Console.ResetColor(); // Reset color to default

        Console.ForegroundColor = ConsoleColor.Blue; // Blue color for bot's name
        Console.WriteLine("==================================================");
        Console.WriteLine(Figgle.FiggleFonts.Small.Render("Cybersecurity Awareness Bot"));
        Console.WriteLine("==================================================");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Blue; // Blue color for slogan
        Console.WriteLine(Figgle.FiggleFonts.Mini.Render("Stay safe online!"));
        Console.WriteLine("==================================================");
        Console.ResetColor();
    }

    static void StartChatbot()
    {
        Console.WriteLine("\nYou can ask me cybersecurity-related questions!");
        Console.WriteLine("Try asking: 'How are you?', 'What's your purpose?', 'What can I ask you about?' or questions about online safety.\n");

        while (true)
        {
            // Simulate typing effect for the bot
            SimulateTyping("You: ");
            string userInput = Console.ReadLine().ToLower();

            // Input Validation: Check for empty input
            if (string.IsNullOrWhiteSpace(userInput))
            {
                SimulateTyping("Bot: I didn't quite understand that. Could you rephrase?");
                continue; // Skip further processing and prompt the user again
            }

            if (userInput == "exit" || userInput == "quit")
            {
                SimulateTyping("\nGoodbye! Stay safe online. 👋");
                break;
            }

            RespondToUser(userInput);
        }
    }

    static void RespondToUser(string userInput)
    {
        // Set the bot's response color to Blue
        Console.ForegroundColor = ConsoleColor.Blue;

        if (userInput.Contains("how are you"))
        {
            SimulateTyping("Bot: I'm doing well, thank you for asking! 😊 I'm here to help you with your cybersecurity questions.");
        }
        else if (userInput.Contains("what's your purpose") || userInput.Contains("what is your purpose"))
        {
            SimulateTyping("Bot: My purpose is to educate you on cybersecurity, helping you stay safe from online threats.");
        }
        else if (userInput.Contains("what can i ask you about") || userInput.Contains("help"))
        {
            SimulateTyping("Bot: You can ask me about:");
            SimulateTyping("- Password safety 🔐");
            SimulateTyping("- Phishing scams 🎣");
            SimulateTyping("- Safe browsing habits 🛡️");
            SimulateTyping("- And general cybersecurity advice!");
        }
        else if (userInput.Contains("password safety") || userInput.Contains("how do i create a strong password"))
        {
            SimulateTyping("Bot: To create a strong password, use a combination of uppercase letters, lowercase letters, numbers, and symbols. Avoid using personal information, and consider using a password manager to generate and store secure passwords.");
        }
        else if (userInput.Contains("phishing") || userInput.Contains("what is phishing"))
        {
            SimulateTyping("Bot: Phishing is a method used by cybercriminals to trick you into giving up personal information, such as login credentials or credit card details. Always verify the source of emails and links before clicking on them.");
        }
        else if (userInput.Contains("safe browsing") || userInput.Contains("how can i browse safely online"))
        {
            SimulateTyping("Bot: To browse safely, make sure your browser is up-to-date, use strong passwords, avoid clicking on suspicious links, and always ensure the website you're visiting is secure (look for 'https://' and a padlock icon).");
        }
        else
        {
            SimulateTyping("Bot: I didn't quite understand that. Could you rephrase?");
        }

        // Reset color after the bot's response
        Console.ResetColor();
    }

    // Method to simulate typing effect for the bot's responses
    static void SimulateTyping(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(50); // Slight delay to simulate typing
        }
        Console.WriteLine(); // Move to the next line after the message
    }
}

