using CoachPosition.Data.Abstract;
using CoachPosition.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoachPosition.Data.Concrete
{
    public class Repository : IRepository
    {
        private EFContext context = new EFContext();
        public IQueryable<Train> Trains
        {
            get { return context.Trains; }
        }

        public void SaveTrain(Train train)
        {
            if (train.TrainID == 0)
            {
                context.Trains.Add(train);
            }
            else
            {
                context.Entry(train).State = EntityState.Modified; // Indicating that the record is changed
            }
            context.SaveChanges();
        }

        public void DeleteTrain(Train train)
        {
            context.Entry(train).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}