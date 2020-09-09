using SearchFight.Core.Impl;
using SearchFight.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SearchFight.Core
{
    public static class SearchCore
    {
        private static readonly IProcessSearch ProcessSearch;

        static SearchCore()
        {
            ProcessSearch = new ProcessSearch();
            var nameLog = string.Format("{0}.log", DateTime.Now.Date.ToString("yyyy-MM-dd"));
            StreamWriter logFile = File.AppendText(nameLog);
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
        }

        public static List<string> GetResultOfSearch(List<string> words)
        {
            return Execute(() =>
            {
                TraceLogStart("GetResultOfSearch", words);
                var listResultSearch = ProcessSearch.GetResultOfSearch(words).Result;
                var listBestResult = ProcessSearch.GetBestResultsInResultSearch(listResultSearch);
                return ProcessSearch.GetResultOfSearchFight(listResultSearch, listBestResult);
            });                      
        }

        #region Control Errors and Logs
        private static T Execute<T>(Func<T> function)
        {
            try
            {
                return function();
            }
            catch (ApplicationException ex)
            {
                Trace.WriteLine(string.Format("Application Exception: {0}", ex.Message));
                return default;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("System exception: {0}", ex.Message));
                return default;
            }
            finally
            {
                TraceLogEnd();
            }
        }

        private static void TraceLogStart(string resource, List<string> parameters)
        {
            Trace.WriteLine(string.Format("Started at {0}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")));
            Trace.WriteLine(string.Format("Resource: {0}", resource));
            if (parameters.Count > 0)
            {
                var message = string.Empty;
                for (var index = 0; index < parameters.Count; index++)
                {
                    message += string.Format(" ({0}) {1}", index, parameters[index]);
                }
                Trace.WriteLine(string.Format("Parameters:{0}", message));
            }
        }

        private static void TraceLogEnd()
        {
            Trace.WriteLine(string.Format("Finished at {0}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")));
        }

        #endregion
    }
}
