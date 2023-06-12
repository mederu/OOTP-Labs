Title: Text Processing Program

Description:
Create a program that takes user input text and provides three text processing options: Remove Duplicate Words, Remove Space Between Words, and Uppercase Text. The program should apply the selected processing options to the input text and output the processed text.

Requirements:

The program should have a TextProcessingCommand class that manages a list of ITextProcessor objects.
- The program should have an ITextProcessor interface that defines a Process method to process the text.
- The program should have three classes that implement the ITextProcessor interface: RemoveDuplicateWordsProcessor, RemoveSpaceBetweenWordsProcessor, and UppercaseProcessor.
- The RemoveDuplicateWordsProcessor class should remove duplicate words from the input text.
- The RemoveSpaceBetweenWordsProcessor class should remove spaces between words in the input text.
- The UppercaseProcessor class should make all the text uppercase.
- The program should prompt the user for input text.
- The program should prompt the user to select processing options (comma-separated).
- The program should execute the selected processing options in the order specified by the user.
- The program should output the processed text.
