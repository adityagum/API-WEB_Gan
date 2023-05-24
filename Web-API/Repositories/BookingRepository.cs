using Microsoft.Identity.Client;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Accounts;
using Web_API.ViewModels.Bookings;

namespace Web_API.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository

    {
        private readonly IRoomRepository _roomRepository;
        public BookingRepository(BookingManagementDbContext context, IRoomRepository roomRepository) : base(context) 
        {
            _roomRepository = roomRepository;
        }

        private int CalculateBookingLength(DateTime startDate, DateTime endDate)
        {
            int totalDays = 0; // Untuk menghitung hari
            DateTime currentDate = startDate.Date; // Perhitungan tergantung dari tanggal yang digunakan

            while (currentDate <= endDate.Date)
            {
                // Mengecek apakah currentDate adalah hari kerja (Selain sabtu dan minggu) 
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    // Jika currentDate hari kerja, totalDays bertambah
                    totalDays++;
                }
                currentDate = currentDate.AddDays(1); // untuk maju ke tanggal berikutnya.
            }

            return totalDays;
        }

        public IEnumerable<BookingLengthVM> GetBookingLength()
        {
            /* Menampung semua data room di var rooms*/
            var rooms = _roomRepository.GetAll();

            /* Mengambil semua data booking*/
            var bookings = GetAll();

            /* Melakukan instance/membuat object untuk setiap pemesanan yang memenuhi kondisi diatas..
               Pada part ini, value RoomName akan diisi dengan nama Room yang dicari berdasarkan RoomGuid. */
            var bookingLengths = bookings.Select(b => new BookingLengthVM
            {
                RoomName = rooms.FirstOrDefault(r => r.Guid == b.RoomGuid)?.Name, // Di set menjadi (?) untuk memastikan tidak terjadi kesalahan ketika objek tidak ditemukan, sehingga valuenya akan otomatis NULL
                BookingLength = CalculateBookingLength(b.StartDate, b.EndDate)
            });

            return bookingLengths;
        }

        
    }
}
