using System;

namespace TouristPlace.Models
{
    public class UnitOfWork : IDisposable
    {
        private TouristContext context = new TouristContext();
        private GenericRepository<ToristPlace> toristPlaceRepository;
        private GenericRepository<TouristComment> touristCommentRepository;
        private GenericRepository<TouristRegistration> touristRegistrationRepository;

        public GenericRepository<ToristPlace> ToristPlaceRepository
        {
            get
            {
                if (this.toristPlaceRepository == null)
                {
                    this.toristPlaceRepository = new GenericRepository<ToristPlace>(context);
                }
                return toristPlaceRepository;
            }
        }

        public GenericRepository<TouristComment> TouristCommentRepository
        {
            get
            {
                if (this.touristCommentRepository == null)
                {
                    this.touristCommentRepository = new GenericRepository<TouristComment>(context);
                }
                return touristCommentRepository;
            }
        }

        public GenericRepository<TouristRegistration> TouristRegistrationRepository
        {
            get
            {
                if (this.touristRegistrationRepository == null)
                {
                    this.touristRegistrationRepository = new GenericRepository<TouristRegistration>(context);
                }
                return touristRegistrationRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}