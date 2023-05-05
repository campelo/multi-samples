using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouping;

internal class GroupService
{
    List<Item> items = new()
    {
        new("A", "1", "1", "value 1"),
        new("A", "1", "2", "value 2"),
        new("A", "2", "1", "value 3"), // duplicated (Unity + Serie + Order) => A 2 1
        new("A", "2", "2", "value 4"),
        new("B", "1", "1", "value 5"), // duplicated (Unity + Serie + Order) => B 1 1
        new("B", "2", "1", "value 6"),
        new("B", "3", "3", "value 7"),
        new("A", "2", "1", "value 8"), // duplicated (Unity + Serie + Order) => A 2 1
        new("B", "1", "1", "value 9"), // duplicated (Unity + Serie + Order) => B 1 1
        new("C", "1", "1", "value 10"),
    };

    internal void GroupingSample()
    {
        var groups = items
                .GroupBy(i => new { i.Unity, i.Serie, i.Order });

        var uniques = groups.Where(x => x.Count() == 1);
        var nonUniques = groups.Where(x => x.Count() > 1);

        List<Item>? listObjects = uniques.Select(group => group.ToList()).Aggregate((a, b) => { a.AddRange(b); return a; }).ToList();

        Console.WriteLine($"{nameof(uniques)} : {uniques.Count()}");
        // uniques : 5

        Console.WriteLine($"{nameof(nonUniques)} : {nonUniques.Count()}");
        // nonUniques : 2
    }

    internal void GroupingSample2()
    {
        var duplicatedGroups = items
                .GroupBy(i => new { i.Unity, i.Serie, i.Order })
                .Where(x => x.Count() > 1);

        Console.WriteLine($"DUPLICATED");
        foreach (var g in duplicatedGroups)
        {
            foreach (var i in g.ToList())
            {
                Console.WriteLine(i);
            }
        }

        var uniques = items.Except(duplicatedGroups.SelectMany(g => g));

        var groupHasNoSequence = uniques.GroupBy(i => (i.Unity, i.Serie))
                            .Where(g => HasNoSequence(g));

        Console.WriteLine($"HAS NO SEQUENCE");
        foreach (var g in groupHasNoSequence)
        {
            foreach (var i in g.ToList())
            {
                Console.WriteLine(i);
            }
        }

        uniques = uniques.Except(groupHasNoSequence.SelectMany(g => g)).ToList();

        Console.WriteLine($"UNIQUES");
        foreach (var i in uniques)
        {
            Console.WriteLine(i);
        }
    }

    internal void GroupingSample3()
    {
        var group1 = items.GroupBy(x => new { x.Unity, x.Serie, x.Order });
        var group2 = group1.GroupBy(x => x.Key.Unity).Where(x => x.Count() > 1);
    }

    private bool HasNoSequence(IGrouping<(string Unity, string Serie), Item> g)
    {
        var s1 = g.Select(i => Convert.ToInt32(i.Order));
        var s2 = Enumerable.Range(1, g.Count());

        return s1.Except(s2).Any();
    }
}
