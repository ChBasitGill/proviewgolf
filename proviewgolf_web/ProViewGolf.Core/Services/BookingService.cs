using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using System.Globalization;
using System.Collections.Generic;

namespace ProViewGolf.Core.Services
{
    public class BookingService
    {
        private readonly IProGolfContext _dbo;
        

        public BookingService(IProGolfContext context)
        {
            _dbo = context;
        }

        public bool AddOrUpdate(Booking booking, out string msg)
        {
            try
            {
                var entity = _dbo.Bookings.AsNoTracking().FirstOrDefault(x =>
                    x.BookingDate.Date == booking.BookingDate &&
                    x.ProRefId == booking.ProRefId);
                msg = "Booking updated successfully";
                if (entity != null)
                {
                    if (entity.StudentRefId != booking.StudentRefId)
                    {
                        msg = "Booking Already exists";
                        return false;
                    }
                    else
                    {
                        booking.BookingId = entity?.BookingId ?? 0;
                        _dbo.Bookings.AddOrUpdate(booking, booking.BookingId);
                    }
                }
                else
                {
                    booking.BookingId = entity?.BookingId ?? 0;
                    _dbo.Bookings.AddOrUpdate(booking, booking.BookingId);
                }
                _dbo.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        public List<Booking> Booking(long studenId, long proRefId)
        {
            string msg = "";
            List<Booking> bookings = new List<Booking>();
            try
            {
                bookings = _dbo.Bookings.AsNoTracking().Where(x =>
                      x.StudentRefId == studenId &&
                     x.ProRefId == proRefId).OrderBy(c => c.BookingDate).ToList();


                return bookings;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return bookings;
            }
        }


    }
}