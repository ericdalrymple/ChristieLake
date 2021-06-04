using System;

public class LeaderboardScore : IComparable<LeaderboardScore>
{
    private Guid m_Id = Guid.NewGuid();

    public Guid Guid
    {
        get { return m_Id; }
    }

    public string Id
    {
        get { return $"{Name}|{m_Id.ToString("D")}"; }
    }

    public string Name
    {
        get;
        private set;
    }

    public int Value
    {
        get;
        private set;
    }

    public LeaderboardScore(string name, int score)
    {
        Name = name;
        Value = score;
    }

    public LeaderboardScore(string id, string score)
    {
        int delimiterPosition = id.IndexOf('|');
        m_Id = Guid.Parse(id.Substring(delimiterPosition + 1));
        Name = id.Substring(0, delimiterPosition);
        Value = int.Parse(score);
    }

    public int CompareTo(LeaderboardScore other)
    {
        // Compare first by score
        int scoreCompare = Math.Sign(other.Value - Value);
        if (scoreCompare != 0)
        {
            return scoreCompare;
        }

        // Compare second by name
        return Name.CompareTo(other.Name);
    }
}
