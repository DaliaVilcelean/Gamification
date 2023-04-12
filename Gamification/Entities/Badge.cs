namespace Gamification.Entities
{
    public class Badge
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
    //    public BadgeType BadgeType { get; set; }
        public Guid UserId { get; set; }
        public virtual List<UserBadge> UserBadges { get; set; }
    }

    public enum BadgeType
    {
        Starter,


        /*

Badge-ul de jucător vechi: acordat utilizatorilor care au fost activi pe platformă pentru o anumită perioadă de timp, de exemplu un an.

Badge-ul de contributor: acordat utilizatorilor care au contribuit la comunitatea de pe platformă prin postarea de comentarii, recenzii sau alte informații utile.

Badge-ul de expert: acordat utilizatorilor care au obținut un scor ridicat sau au dat răspunsuri corecte la un număr mare de întrebări.

Badge-ul de învingător: acordat utilizatorilor care au câștigat un număr mare de jocuri sau turnee.*/
    }
}
