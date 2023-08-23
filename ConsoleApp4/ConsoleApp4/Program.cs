using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

public class DesignProject
{
    public string ProjectName { get; set; }
    private int totalScreens;
    protected bool isResponsive;
    internal string primaryColor;
    private readonly string designerName;

    public DesignProject(string name, string designer)
    {
        ProjectName = name;
        designerName = designer;
        totalScreens = 0;
        isResponsive = false;
        primaryColor = "#FFFFFF";
    }

    public void AddScreen()
    {
        totalScreens++;
    }

    private void MakeResponsive()
    {
        isResponsive = true;
    }

    protected void ChangePrimaryColor(string color)
    {
        primaryColor = color;
    }

    public override string ToString()
    {
        return $"Design Project: {ProjectName}, Designer: {designerName}, Total Screens: {totalScreens}, Is Responsive: {isResponsive}, Primary Color: {primaryColor}";
    }
}


class Program
{
    static void Main(string[] args)
    {
        DesignProject project = new DesignProject("New Project", "John Doe");

        // Work with Type and TypeInfo
        Type type = typeof(DesignProject);
        Console.WriteLine($"Type Name: {type.Name}");
        TypeInfo typeInfo = type.GetTypeInfo();
        Console.WriteLine($"Type Namespace: {typeInfo.Namespace}");

        // Work with MemberInfo
        MemberInfo[] members = type.GetMembers();
        foreach (var member in members)
        {
            Console.WriteLine($"Member: {member.Name}, Type: {member.MemberType}");
        }

        // Work with FieldInfo
        FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            Console.WriteLine($"Field: {field.Name}, Type: {field.FieldType}");
        }

        // Work with MethodInfo and invoke a method
        MethodInfo addScreenMethod = type.GetMethod("AddScreen");
        addScreenMethod.Invoke(project, null);

        Console.WriteLine(project);
    }
}