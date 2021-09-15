
namespace RetSimWeb;
public class TestService
{
    public int MyMethod(int parameter)
    {
        int i = 0;
        while (i < 5000000) i += (i * parameter);
        return i;
    }
}
