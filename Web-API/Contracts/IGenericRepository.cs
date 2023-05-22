namespace Web_API.Contracts;

/*
 * Folder contracs ini digunakan untuk menampung interface
 * Contracts disini bertugas sebagai penghubung antara controllers dan repositories, jadi
 * ketika controller ingin menggunakan salah satu repositories, dia harus melalui contracts (*DIsebut juga Dependency Injection).
 * Dalam sisi OOP, folder ini sama dengan abstract yaitu kita menyembunyikan detail. Singkatnya Controller
 * tidak perlu tau isi dari Repositories
 */
public interface IGenericRepository <T> where T : class
{
    T? Create(T t); // Dibuat nullable karena bisa saja dia bermasalah ketika menginputkan
    bool Update(T t);
    bool Delete(Guid guid);
    IEnumerable<T> GetAll();
    T GetByGuid(Guid guid);
  /*  T GetByName(T t);*/

   

}
