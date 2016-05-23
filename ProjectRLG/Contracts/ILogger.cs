namespace ProjectRLG.Contracts
{
    using System;

    public interface ILogger
    {
        void Info(string message, string category);
        void Warn(string message, string category);
        void Error(string message, string category);
        void Error(string message, Exception ex, string category);
    }
}
