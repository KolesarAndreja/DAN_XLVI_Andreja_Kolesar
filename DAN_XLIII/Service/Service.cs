using DAN_XLIII.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLIII.Service
{
    class Service
    {

        #region GET SPECIFIC EMPLOYEE
        public static tblEmployee GetEmployeeByUsernameAndPsw(string username, string password)
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                    tblEmployee result = (from x in context.tblEmployees where x.username == username && x.password == password select x).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }

        public static tblManager GetManagerById(int id)
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                    tblManager result = (from x in context.tblManagers where x.employeeId == id select x).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }

        }
        #endregion

        public static bool isManager(tblEmployee e)
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                    tblManager result = (from x in context.tblManagers where x.employeeId == e.employeeId select x).FirstOrDefault();
                    if (result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        #region READ ALL EMPLOYEES
        public static List<tblEmployee> GetAllEmployees()
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                    List<tblEmployee> list = new List<tblEmployee>();
                    list = (from x in context.tblEmployees select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        #endregion

        #region ADD
        /// <summary>
        /// Add new employee or manager to db. 
        /// </summary>
        /// <param name="employee">Add this employee to db</param>
        /// <param name="manager">true if this employee is also and manager</param>
        public static tblEmployee AddNewEmployeeOrManager(tblEmployee employee, bool manager)
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                    if (employee.employeeId == 0)
                    {
                        //add into tblEmployee
                        tblEmployee newEmployee = new tblEmployee();
                        newEmployee.firstname = employee.firstname;
                        newEmployee.lastname = employee.lastname;
                        newEmployee.dateOfBirth = employee.dateOfBirth;
                        newEmployee.jmbg = employee.jmbg;
                        newEmployee.accountNumber = employee.accountNumber;
                        newEmployee.mail = employee.mail;
                        newEmployee.salary = employee.salary;
                        newEmployee.position = employee.position;
                        newEmployee.username = employee.username;
                        newEmployee.password = employee.password;
                        context.tblEmployees.Add(newEmployee);
                        context.SaveChanges();
                        employee.employeeId = newEmployee.employeeId;

                        if (manager)
                        {
                            //add into tblManager
                            tblManager newManager = new tblManager();
                            newManager.employeeId = employee.employeeId;
                            newManager.sector = employee.sector;
                            newManager.access = employee.access;
                            context.tblManagers.Add(newManager);
                            context.SaveChanges();
                        }
                        return employee;
                    }
                    else
                    {
                        tblEmployee employeeToEdit = (from x in context.tblEmployees where x.employeeId == employee.employeeId select x).FirstOrDefault();
                        employeeToEdit.firstname = employee.firstname;
                        employeeToEdit.lastname = employee.lastname;
                        employeeToEdit.dateOfBirth = employee.dateOfBirth;
                        employeeToEdit.jmbg = employee.jmbg;
                        employeeToEdit.accountNumber = employee.accountNumber;
                        employeeToEdit.mail = employee.mail;
                        employeeToEdit.salary = employee.salary;
                        employeeToEdit.position = employee.position;
                        employeeToEdit.username = employee.username;
                        employeeToEdit.password = employee.password;
                        context.SaveChanges();
                        return employee;
                    }


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region DELETE EMPLOYEE
        public static void DeleteEmployee(tblEmployee emp)
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                    if (isManager(emp))
                    {
                        tblManager managerToDelete = (from u in context.tblManagers where u.employeeId == emp.employeeId select u).First();
                        context.tblManagers.Remove(managerToDelete);
                        context.SaveChanges();
                    }

                    tblEmployee userToDelete = (from u in context.tblEmployees where u.employeeId == emp.employeeId select u).First();
                    context.tblEmployees.Remove(userToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        #endregion

        #region ADD REPORT
        public static string AddReport(vwReport report)
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                  
                    if (report.reportId == 0)
                    {
                        int result = (from x in context.tblReports where x.employeeId == report.employeeId && x.reportDate == report.reportDate select x).Count();
                        if (result < 2)
                        {
                            int hours = 0;
                            if (result == 1)
                            {
                                hours = (from x in context.tblReports where x.employeeId == report.employeeId && x.reportDate == report.reportDate select x.workingHours).FirstOrDefault();
                            }

                            if (hours + report.workingHours <= 12)
                            {
                                tblReport newReport = new tblReport();
                                newReport.employeeId = report.employeeId;
                                newReport.reportDate = report.reportDate;
                                newReport.project = report.project;
                                newReport.workingHours = report.workingHours;
                                context.tblReports.Add(newReport);
                                context.SaveChanges();
                                return null;
                            }
                            
                        }
                    }
                    else 
                    {
                        tblReport reportToEdit = (from x in context.tblReports where x.reportId == report.reportId select x).FirstOrDefault();
                        reportToEdit.reportDate = report.reportDate;
                        reportToEdit.project = report.project;
                        reportToEdit.workingHours = report.workingHours;
                        int r = (from x in context.tblReports where x.employeeId == report.employeeId && x.reportDate ==report.reportDate && x.reportId !=report.reportId select x.workingHours).FirstOrDefault();
                        if (r == 0 && reportToEdit.workingHours<=12)
                        {
                            context.SaveChanges();
                            return null;
                        }
                        else if(r + reportToEdit.workingHours <= 12)
                        {
                           context.SaveChanges();
     
                        }
                        else
                        {
                            return "Too many working hours";
                        }

                    }
                }
                return "Too many working hours";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return "Invalid report";
            }
        }
        #endregion

        #region READ ALL REPORTS
        public static List<vwReport> GetAllReports()
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                    List<vwReport> list = new List<vwReport>();
                    list = (from x in context.vwReports select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        #endregion

        #region DELETE REPORT
        public static void DeleteReport(vwReport report)
        {
            try
            {
                using (DataRecordsEntities1 context = new DataRecordsEntities1())
                {
                    tblReport reportToDelete = (from u in context.tblReports where u.reportId == report.reportId select u).First();
                    context.tblReports.Remove(reportToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        #endregion
    }
}
