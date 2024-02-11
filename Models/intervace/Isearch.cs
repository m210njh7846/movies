namespace books.Models.intervace
{
    public interface Isearch<TEntity>
    {

        void delete(int id);
        List<TEntity> Search(string term);
    }
}
