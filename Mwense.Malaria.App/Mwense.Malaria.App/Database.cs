using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mwense.Malaria.App.Models;
using System.Linq;
using SQLite;

namespace Mwense.Malaria.App
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Users>().Wait();
            _database.CreateTableAsync<Patients>().Wait();
            _database.CreateTableAsync<LabResults>().Wait();
            _database.CreateTableAsync<FollowUp>().Wait();
            _database.CreateTableAsync<NotFollowedUp>().Wait();
            _database.CreateTableAsync<FollowUpList>().Wait();
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var data = await _database.Table<Users>()
                            .Where(i => i.Username == userName && i.Password == password)
                            .FirstOrDefaultAsync();

            if (data != null)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<List<FollowUpList>>FollowUpAsync(string results, DateTime date)
        {
            return await _database.Table<FollowUpList>()
                            .Where(i => i.MalariaResults == results && i.TestDate == date && i.IsReviewed == false).ToListAsync();
            //var Patients = await _database.Table<Patients>().ElementAtAsync()
            //return await _database.QueryAsync<FollowUpList>("SELECT * FROM [FollowUpList] WHERE [MalariaResults] = 'Postive'");
        }
        /*public async Task<List<FollowUpList>>TryFollowupAsync()
        {
            var lab = FollowUpAsync();
            var pat = GetPatientsAsync();

            return await from s in lab // outer sequence
                         join st in pat //inner sequence 
                         on s.OPDNumber equals st.OPDNumber // key selector 
                         select new FollowUpList
                         { // result selector 
                             FirstName = st.FirstName,
                             MalariaResults = s.MalariaResults
                         };
            //List<FollowUpList> FollowUpLists = new List<FollowUpList>();
            //var q = await _database.QueryAsync<LabResults>(@"SELECT * FROM LabResults LR INNER JOIN Patients P ON LR.OPDNumber = P.OPDNumber WHERE LR.MalariaResults = 'Postive'");
            //return (List<FollowUpList>)q.Select(x => new FollowUpList { FirstName = x.Patients.FirstName, MalariaResults = x.MalariaResults, TestDate = x.TestDate });
        }*/

        public async Task<bool> SubmitResultsAsync(LabResults Labresult)
        {
            //var Submit = await _database.InsertAsync(Labresult);
            if (Labresult.OPDNumber != 0)
            {
                await _database.InsertAsync(Labresult);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CreateUserAsync(Users user)
        {
            //var Submit = await _database.InsertAsync(Labresult);
            if (user.Username != null)
            {
                await _database.InsertAsync(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckUserAsync(string username)
        {
            var result = await _database.Table<Users>().Where(i => i.Username == username).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            else 
                return false;
        }

        public async Task<bool> CheckClientAsync(int opdnumber)
        {
            var result = await _database.Table<Patients>().Where(i => i.OPDNumber == opdnumber).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<bool> CreatePatientAsync(Patients patient)
        {
            //var Submit = await _database.InsertAsync(Labresult);
            if (patient.OPDNumber != 0)
            {
                await _database.InsertAsync(patient);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Patients>> GetPatientsAsync()
        {
            //Get all Registered Patients.
            return await _database.Table<Patients>().ToListAsync();
        }

        public Task<Patients> GetPatientAsync(int Opdnumber)
        {
            // Get a specific note.
            return _database.Table<Patients>()
                            .Where(i => i.OPDNumber == Opdnumber)
                            .FirstOrDefaultAsync();
 
        }

        public async Task<int> SavePatientAsync(Patients patient)
        {
            if (patient.OPDNumber != 0)
            {
                // Update an existing note.
                return  await _database.UpdateAsync(patient);
            }
            else
            {
                // Save a new note.
                return await _database.InsertAsync(patient);
            }
        }

        public async Task<int> DeletePatientAsync(Patients patient)
        {
            // Delete a note.
            return await _database.DeleteAsync(patient);
        }

        /*public async Task<List<LabResults>> GetFollowupPatientsAsync()
        {
            //Get all Registered Patients.
            //return _database.Table<Patients>().ToListAsync();
            return await _database.Table<LabResults>().ToListAsync();
  
        }*/

        public async Task<bool> SubmitFollowupAsync(FollowUpList followUpList)
        {
            //var Submit = await _database.InsertAsync(Labresult);
            if (followUpList.OPDNumber != 0)
            {
                await _database.InsertAsync(followUpList);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<FollowUpList> GetFollowupAsync(int Opdnumber, DateTime dateTime)
        {
            return _database.Table<FollowUpList>()
                            .Where(i => i.OPDNumber == Opdnumber && i.TestDate == dateTime)
                            .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateFollowupAsync(int Id)
        {
            await _database.UpdateAsync(new FollowUpList { ID = Id, IsReviewed = true});
            return true;      
        }


        public async Task<bool> SubmitReviewedAsync(FollowUp followUp)
        {
            //var Submit = await _database.InsertAsync(Labresult);
            if (followUp.OPDNumber != 0)
            {
                await _database.InsertAsync(followUp);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}