Task: Develop a simple program that allows a user to generate reports based on different data sources (CSV files, databases). The program should have a modular architecture that allows for easy integration of new data sources and report generation strategies.

Design:

The program should use the Strategy pattern to allow the user to select different report generation strategies (PDF, HTML, CSV).

The program should use the Observer pattern to notify the user when a report generation process has started, finished, or encountered an error.

The program should use the Command pattern to encapsulate report generation requests and allow for their queuing, cancellation, and undoing.
