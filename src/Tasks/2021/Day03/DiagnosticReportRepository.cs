using System;

namespace App.Tasks.Year2021.Day3
{
    public class DiagnosticReportRepository
    {
        public string[] GetDiagnosticReport(string input)
        {
            string[] diagnosticReport = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return diagnosticReport;
        }
    }
}
