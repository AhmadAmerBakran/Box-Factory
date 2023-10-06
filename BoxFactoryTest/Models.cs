namespace BoxFactoryTest;

public class Box
{
    public int Id { get; set; }
    
    public string BoxName { get; set; }
    
    public double Price { get; set; }
    
    public double BoxWidth { get; set; }
    
    public double BoxLength { get; set; }
    
    public double BoxHeight { get; set; }
    
    public double BoxThickness { get; set; }
    
    public string BoxColor { get; set; }
    
}

public class SearchBox
{
    public string BoxName { get; set; }
    public double Price { get; set; }
    public string BoxColor { get; set; }
    
}