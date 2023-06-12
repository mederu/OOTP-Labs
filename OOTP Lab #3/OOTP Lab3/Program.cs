using System;
using System.Collections.Generic;


interface IReportGenerator
{
    void GenerateReport(string[] data);
}

class PDFReportGenerator : IReportGenerator
{
    public void GenerateReport(string[] data)
    {
        Console.WriteLine("Generating PDF report from data: " + string.Join(",", data));
    }
}

class HTMLReportGenerator : IReportGenerator
{
    public void GenerateReport(string[] data)
    {
        Console.WriteLine("Generating HTML report from data: " + string.Join(",", data));
    }
}

class CSVReportGenerator : IReportGenerator
{
    public void GenerateReport(string[] data)
    {
        Console.WriteLine("Generating CSV report from data: " + string.Join(",", data));
    }
}
class ReportGenerationEvent : EventArgs
{
    public string Message { get; set; }
}

class ReportGenerationCommand
{
    private IReportGenerator _generator;
    private string[] _data;

    public ReportGenerationCommand(IReportGenerator generator, string[] data)
    {
        _generator = generator;
        _data = data;
    }

    public void Execute()
    {
        OnReportGenerationEvent(new ReportGenerationEvent { Message = "Report generation process started." });
        try
        {
            _generator.GenerateReport(_data);
            OnReportGenerationEvent(new ReportGenerationEvent { Message = "Report generation process finished." });
        }
        catch (Exception ex)
        {
            OnReportGenerationEvent(new ReportGenerationEvent { Message = $"Report generation process encountered an error: {ex.Message}" });
        }
    }
    public event EventHandler<ReportGenerationEvent> ReportGenerationEvent;

    protected virtual void OnReportGenerationEvent(ReportGenerationEvent e)
    {
        ReportGenerationEvent?.Invoke(this, e);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var pdfCommand = new ReportGenerationCommand(new PDFReportGenerator(), new[] { "pdf data" });
        var htmlCommand = new ReportGenerationCommand(new HTMLReportGenerator(), new[] { "html data" });
        var csvCommand = new ReportGenerationCommand(new CSVReportGenerator(), new[] { "csv data" });

        pdfCommand.ReportGenerationEvent += OnReportGenerationEvent;
        htmlCommand.ReportGenerationEvent += OnReportGenerationEvent;
        csvCommand.ReportGenerationEvent += OnReportGenerationEvent;

        pdfCommand.Execute();
        htmlCommand.Execute();
        csvCommand.Execute();
    }

    static void OnReportGenerationEvent(object sender, ReportGenerationEvent e)
    {
        Console.WriteLine(e.Message);
    }
}