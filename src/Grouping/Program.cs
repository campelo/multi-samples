using Grouping;

List<Item> items = new()
{
    new("A","1","1","value 1"), 
    new("A","1","2","value 2"),
    new("A","2","1","value 3"), // duplicated (Unity + Serie + Order) => A 2 1
    new("A","2","2","value 4"),
    new("B","1","1","value 5"), // duplicated (Unity + Serie + Order) => B 1 1
    new("B","2","1","value 6"),
    new("B","3","3","value 7"),
    new("A","2","1","value 8"), // duplicated (Unity + Serie + Order) => A 2 1
    new("B","1","1","value 9"), // duplicated (Unity + Serie + Order) => B 1 1
};

var groups = items
        .GroupBy(i => new { i.Unity, i.Serie, i.Order });

var uniques = groups.Where(x => x.Count() == 1);
var nonUniques = groups.Where(x => x.Count() > 1);

List<Item>? listObjects = uniques.Select(group => group.ToList()).Aggregate((a, b) => { a.AddRange(b); return a; }).ToList();

Console.WriteLine($"{nameof(uniques)} : {uniques.Count()}");
// uniques : 5

Console.WriteLine($"{nameof(nonUniques)} : {nonUniques.Count()}");
// nonUniques : 2