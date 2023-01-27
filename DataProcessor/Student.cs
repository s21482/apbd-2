
namespace DataProcessor
{
   class Student : IEqualityComparer<Student>
{
    public string IndexNumber { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public string Birthdate { get; set; }
    public string Email { get; set; }
    public string MothersName { get; set; }
    public string FathersName { get; set; }
    public Studies Studies { get; set; }

    public bool Equals(Student x, Student y)
    {
        return x.Fname == y.Fname && x.Lname == y.Lname && x.IndexNumber == y.IndexNumber;
    }

    public int GetHashCode(Student obj)
    {
        return obj.Fname.GetHashCode() ^ obj.Lname.GetHashCode() ^ obj.IndexNumber.GetHashCode();
    }
    }

class Studies
{
    public string Name { get; set; }
    public string Mode { get; set; }
}
}
