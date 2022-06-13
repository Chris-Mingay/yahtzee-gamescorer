using System.Collections.Concurrent;
using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Monitoring;

public class EFLogger : DbCommandInterceptor
    {
        private static readonly ConcurrentDictionary<Guid, DateTime> _startTimes = new ConcurrentDictionary<Guid, DateTime>();

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader reader)
        {
            Log(command, eventData);
            return reader;
        }

        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            Log(command, eventData);
            return result;
        }

        public override object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
        {
            Log(command, eventData);
            return result;
        }

        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            Log(command, eventData);
        }

        private static void Log(DbCommand command, CommandEventData eventData)
        {
            TimeSpan? duration = null;
            if (_startTimes.TryRemove(eventData.CommandId, out DateTime startTime))
                duration = DateTime.Now - startTime;

            var parameters = new StringBuilder();
            foreach (DbParameter param in command.Parameters)
            {
                parameters.AppendLine(param.ParameterName + " " + param.DbType + " = " + param.Value);
            }

            var rnd = new Random();

            if(duration?.TotalMilliseconds > 500 || rnd.Next(2) < 2)
            {
                string message = $"Database call {(eventData is CommandErrorEventData ? "FAILED" : "succeeded")} in {duration?.TotalMilliseconds ?? -1:N3} ms. CommandId {eventData.CommandId} \r\nCommand:\r\n{parameters}{command.CommandText}";
                Console.WriteLine(message);
            }

        }

        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            OnStart(eventData.CommandId);
            return result;
        }
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            OnStart(eventData.CommandId);
            return result;
        }

        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            OnStart(eventData.CommandId);
            return result;
        }

        private void OnStart(Guid commandId)
        {
            _startTimes.TryAdd(commandId, DateTime.Now);
        }
    }