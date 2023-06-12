using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public abstract class TextProcessorTemplate
{
    protected abstract string ProcessText(string text);

    public string Process(string text)
    {
        text = PreProcess(text);
        text = ProcessText(text);
        text = PostProcess(text);
        return text;
    }

    protected virtual string PreProcess(string text)
    {
        return text;
    }

    protected virtual string PostProcess(string text)
    {
        return text;
    }
}

public class RemoveDuplicateWordsProcessor : TextProcessorTemplate
{
    protected override string ProcessText(string text)
    {
        // Розбиття тексту на слова
        string[] words = text.Split(' ');

        // Створює HashSet щоб відстежувати слова, які ми вже прочитали з стрінга
        HashSet<string> uniqueWords = new HashSet<string>();

        // Перебирає слова і дає їм хеш, якщо ми ще не зустрічали їх
        List<string> processedWords = new List<string>();
        foreach (string word in words)
        {
            if (!uniqueWords.Contains(word))
            {
                uniqueWords.Add(word);
                processedWords.Add(word);
            }
        }

        // З'єднує оброблені слова обратно в рядок
        string processedText = string.Join(" ", processedWords);

        return processedText;
    }
}

public class RemoveSpaceBetweenWordsProcessor : TextProcessorTemplate
{
    protected override string ProcessText(string input)
    {
        string output = Regex.Replace(input, @"(?<=\S)\s+(?=\S)", "");
        return output;
    }
}

public class UppercaseProcessor : TextProcessorTemplate
{
    protected override string ProcessText(string text)
    {
        string processedText = text.ToUpper();
        return processedText;
    }
}

public class TextProcessingCommand
{
    private List<TextProcessorTemplate> _processors;

    public TextProcessingCommand()
    {
        _processors = new List<TextProcessorTemplate>();
    }

    public void AddProcessor(TextProcessorTemplate processor)
    {
        _processors.Add(processor);
    }

    public string Execute(string text)
    {
        string processedText = text;

        foreach (TextProcessorTemplate processor in _processors)
        {
            processedText = processor.Process(processedText);
        }

        return processedText;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створює об'єкт команди
        TextProcessingCommand textProcessingCommand = new TextProcessingCommand();

        // Прийняття тексту
        Console.WriteLine("Enter the text to be processed:");
        string text = Console.ReadLine();

        // Вибірка процесу
        Console.WriteLine("Select processing options (comma-separated):");
        Console.WriteLine("1. Remove Duplicate Words");
        Console.WriteLine("2. Remove Space Between Words");
        Console.WriteLine("3. Uppercase Text");
        string optionsInput = Console.ReadLine();
        string[] options = optionsInput.Split(',');

        // Вибірка процесу та його реалізація
        foreach (string option in options)
        {
            if (option == "1")
            {
                textProcessingCommand.AddProcessor(new RemoveDuplicateWordsProcessor());
            }
            else if (option == "2")
            {
                textProcessingCommand.AddProcessor(new RemoveSpaceBetweenWordsProcessor());
            }
            else if (option == "3")
            {
                textProcessingCommand.AddProcessor(new UppercaseProcessor());
            }
            else
            {
                Console.WriteLine("Invalid processing option: " + option);
            }
        }

        string processedText = textProcessingCommand.Execute(text);

        // Виведення результату
        Console.WriteLine("Processed text: ");
        Console.WriteLine(processedText);
    }
}