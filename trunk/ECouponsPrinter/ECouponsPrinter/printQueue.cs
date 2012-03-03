using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Printing;
using System.Windows.Forms;

namespace ECouponsPrinter
{
    class printQueue
    {
        public Dictionary<string, int> GetAllPrinterQueues()
        {
            Dictionary<string, int> TempDict = new Dictionary<string, int>();

            PrintServer myPrintServer = new PrintServer(); // Get all the printers installed on this PC

            // List the print server"s queues
            PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();
            String printQueueNames = "My Print Queues:\n\n";
            foreach (PrintQueue pq in myPrintQueues)
            {
                Application.DoEvents();
                pq.Refresh();
                if (pq.Name != null)
                {
                    int PGcount = 0;
                    try
                    {
                        if (pq.NumberOfJobs > 0)
                        {
                            // We know there are jobs.  So we *have* to be able to get the collection at some point
                            DateTime Bailout = DateTime.Now.AddSeconds(5); // Keep trying for 10 seconds or until I get a valid response
                            string ErrMsg = "not yet retreived";
                            while (Bailout > DateTime.Now && ErrMsg != string.Empty)
                            {                              
                                try
                                {
                                    PrintJobInfoCollection Jobs = pq.GetPrintJobInfoCollection();
                                    Application.DoEvents();
                                    foreach (PrintSystemJobInfo Job in Jobs)
                                    {
                                        PGcount += Job.NumberOfPages;
                                        ErrMsg = string.Empty;
                                    }
                                }
                                catch (Exception k)
                                {
                                    ErrMsg = k.Message;
                                    MessageBox.Show(pq.Name, ErrMsg);
                                }
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Exception dork");
                    }
                    TempDict.Add(pq.Name, PGcount);
                }
            }
            return TempDict;
        }

        public bool CanelAllPrintJob()
        {
            // Variable declarations.            
            bool isActionPerformed = false;
            string searchQuery;
            String jobName;
            char[] splitArr;
            int prntJobID;
            ManagementObjectSearcher searchPrintJobs;
            ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.                
                searchQuery = "SELECT * FROM Win32_PrintJob";
                // Create an object using the above query.                
                searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                // Fire the query to get the collection of the printer jobs.                
                prntJobCollection = searchPrintJobs.Get();
                // Look for the job you want to delete/cancel.        
                if (prntJobCollection.Count == 0)
                {
                    return true;
                }
                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    // Job name would be of the format [Printer name], [Job ID]                    
                    splitArr = new char[1];
                    splitArr[0] = Convert.ToChar(",");
                    // Get the job ID.                    
                    prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                    // If the Job Id equals the input job Id, then cancel the job.                                    
                    prntJob.Delete();
                    isActionPerformed = true;
                }
                return isActionPerformed;
            }
            catch (Exception sysException)
            {
                ErrorLog.log(sysException);               
                return false;
            }
        }

        private bool CancelPrintJob(int printJobID)
        {
            // Variable declarations.            
            bool isActionPerformed = false;
            string searchQuery;
            String jobName;
            char[] splitArr;
            int prntJobID;
            ManagementObjectSearcher searchPrintJobs;
            ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.                
                searchQuery = "SELECT * FROM Win32_PrintJob";
                // Create an object using the above query.                
                searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                // Fire the query to get the collection of the printer jobs.                
                prntJobCollection = searchPrintJobs.Get();
                // Look for the job you want to delete/cancel.                
                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    // Job name would be of the format [Printer name], [Job ID]                    
                    splitArr = new char[1];
                    splitArr[0] = Convert.ToChar(",");
                    // Get the job ID.                    
                    prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                    // If the Job Id equals the input job Id, then cancel the job.                    
                    if (prntJobID == printJobID)
                    {
                        // Performs a action similar to the cancel                        
                        // operation of windows print console                        
                        prntJob.Delete();
                        isActionPerformed = true;
                        break;
                    }
                }
                return isActionPerformed;
            }
            catch (Exception sysException)
            {
                ErrorLog.log(sysException);            
                return false;
            }
        }

    }
}
