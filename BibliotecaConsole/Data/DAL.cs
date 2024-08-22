namespace Data;

public class DAL<T> where T : class
{
    public readonly BibliotecaContext context;

    public DAL(BibliotecaContext context)
    {
        this.context = context;
    }

    public IEnumerable<T> List()
    {
        return context.Set<T>().ToList();
    }
    public void add(T objeto)
    {
        context.Set<T>().Add(objeto);
        context.SaveChanges();
    }
    public void Update(T objeto)
    {
        context.Set<T>().Update(objeto);
        context.SaveChanges();
    }
    public void Delete(T objeto)
    {
        context.Set<T>().Remove(objeto);
        context.SaveChanges();
    }
    public T? GetBy(Func<T, bool> cond)
    {
        return context.Set<T>().FirstOrDefault(cond);
    }
}
