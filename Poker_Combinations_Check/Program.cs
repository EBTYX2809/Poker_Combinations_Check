using System;
public class Card
{
    public string Name { get; set; }
    public string Suit { get; set; }
    public int Value { get; set; }

    public Card(string name, string suit, int value)
    {
        Name = name;
        Suit = suit;
        Value = value;
    }
}

class Program
{
    static public void Main(string[] args)
    {
        List<Card> Deck = new List<Card>
        {
            // Hearts
            new Card("2 Hearts", "Hearts", 2),
            new Card("3 Hearts", "Hearts", 3),
            new Card("4 Hearts", "Hearts", 4),
            new Card("5 Hearts", "Hearts", 5),
            new Card("6 Hearts", "Hearts", 6),
            new Card("7 Hearts", "Hearts", 7),
            new Card("8 Hearts", "Hearts", 8),
            new Card("9 Hearts", "Hearts", 9),
            new Card("10 Hearts", "Hearts", 10),
            new Card("Jack Hearts", "Hearts", 11),
            new Card("Queen Hearts", "Hearts", 12),
            new Card("King Hearts", "Hearts", 13),
            new Card("Ace Hearts", "Hearts", 14),
            
            // Diamonds
            new Card("2 Diamonds", "Diamonds", 2),
            new Card("3 Diamonds", "Diamonds", 3),
            new Card("4 Diamonds", "Diamonds", 4),
            new Card("5 Diamonds", "Diamonds", 5),
            new Card("6 Diamonds", "Diamonds", 6),
            new Card("7 Diamonds", "Diamonds", 7),
            new Card("8 Diamonds", "Diamonds", 8),
            new Card("9 Diamonds", "Diamonds", 9),
            new Card("10 Diamonds", "Diamonds", 10),
            new Card("Jack Diamonds", "Diamonds", 11),
            new Card("Queen Diamonds", "Diamonds", 12),
            new Card("King Diamonds", "Diamonds", 13),
            new Card("Ace Diamonds", "Diamonds", 14),
            
            // Clubs
            new Card("2 Clubs", "Clubs", 2),
            new Card("3 Clubs", "Clubs", 3),
            new Card("4 Clubs", "Clubs", 4),
            new Card("5 Clubs", "Clubs", 5),
            new Card("6 Clubs", "Clubs", 6),
            new Card("7 Clubs", "Clubs", 7),
            new Card("8 Clubs", "Clubs", 8),
            new Card("9 Clubs", "Clubs", 9),
            new Card("10 Clubs", "Clubs", 10),
            new Card("Jack Clubs", "Clubs", 11),
            new Card("Queen Clubs", "Clubs", 12),
            new Card("King Clubs", "Clubs", 13),
            new Card("Ace Clubs", "Clubs", 14),
            
            // Spades
            new Card("2 Spades", "Spades", 2),
            new Card("3 Spades", "Spades", 3),
            new Card("4 Spades", "Spades", 4),
            new Card("5 Spades", "Spades", 5),
            new Card("6 Spades", "Spades", 6),
            new Card("7 Spades", "Spades", 7),
            new Card("8 Spades", "Spades", 8),
            new Card("9 Spades", "Spades", 9),
            new Card("10 Spades", "Spades", 10),
            new Card("Jack Spades", "Spades", 11),
            new Card("Queen Spades", "Spades", 12),
            new Card("King Spades", "Spades", 13),
            new Card("Ace Spades", "Spades", 14)
        };

        List<Card> PlayerHand = new List<Card>();

        // Add cards from deck to player's hand with searching by name(it's need to set with randomizer)
        PlayerHand.Add(Deck.Find(a => "King Diamonds" == a.Name) ?? throw new InvalidOperationException("Card not found"));
        PlayerHand.Add(Deck.Find(a => "3 Clubs" == a.Name) ?? throw new InvalidOperationException("Card not found"));
        PlayerHand.Add(Deck.Find(a => "4 Hearts" == a.Name) ?? throw new InvalidOperationException("Card not found"));
        PlayerHand.Add(Deck.Find(a => "9 Hearts" == a.Name) ?? throw new InvalidOperationException("Card not found"));
        PlayerHand.Add(Deck.Find(a => "Jack Diamonds" == a.Name) ?? throw new InvalidOperationException("Card not found"));

        foreach (Card card in PlayerHand)
        {
            Console.WriteLine(card.Name);
        }
        Console.WriteLine();
        CombinationChecking(PlayerHand);
    }

    // Counter repeat cards for easy check for Pair, Thwo Pairs, Trio, Quadro and Full House
    static private Dictionary<int, int> Counter(List<Card> hand)
    {
        Dictionary<int, int> counter = new Dictionary<int, int>();
        foreach (Card card in hand)
        {
            if (counter.ContainsKey(card.Value))
            {
                counter[card.Value]++;
            }
            else
            {
                counter[card.Value] = 1;
            }
        }
        return counter;
    }

    // Function for simple realisation of combination precedence through if else structure
    static private void CombinationChecking(List<Card> hand)
    {
        if (RoyalFlash(hand))
        {
            return;
        }
        else if (StreetFlash(hand))
        {
            return;
        }
        else if (Quadro(hand))
        {
            return;
        }
        else if (FullHouse(hand))
        {
            return;
        }
        else if (Flash(hand))
        {
            return;
        }
        else if (Street(hand))
        {
            return;
        }
        else if (Trio(hand))
        {
            return;
        }
        else if (TwoPairs(hand))
        {
            return;
        }
        else if (Pair(hand))
        {
            return;
        }
        else if (HigherCard(hand))
        {
            return;
        }
    }

    // Functions for determining combinations
    static private bool HigherCard(List<Card> hand)
    {
        // Simple searching from max value
        int max = 0;
        foreach (Card card in hand)
        {
            if (card.Value > max)
            {
                max = card.Value;
            }
        }
        Console.WriteLine($"Higher Card: {hand.Find(a => a.Value == max)?.Name}");
        return true;
    }
    static private bool Pair(List<Card> hand)
    {
        // Searching card.Value == 2 in counted hand makes it easier to find combinations with duplicate values
        foreach (var card in Counter(hand))
        {
            if (card.Value == 2)
            {
                List<Card> combo = hand.FindAll(a => a.Value == card.Key); // Add pair in temp combo list
                Console.WriteLine($"Pair: {combo[0].Name} {combo[1].Name}");
                return true;
            }
        }
        return false;
    }
    static private bool TwoPairs(List<Card> hand)
    {
        // Same as with 1 pair
        int i = 0;
        foreach (var card in Counter(hand))
        {
            if (card.Value == 2)
            {
                List<Card> combo = hand.FindAll(a => a.Value == card.Key);
                i++;
                // But here i skip last checked element for continue searching a second pair after detected first
                foreach (var card2 in Counter(hand).Skip(i))
                {
                    if (card2.Value == 2)
                    {
                        combo.AddRange(hand.FindAll(a => a.Value == card2.Key));
                        Console.WriteLine($"Two pairs: {combo[0].Name} {combo[1].Name} and {combo[2].Name} {combo[3].Name}");
                        return true;
                    }
                }
            }
            i++;
        }
        return false;
    }
    static private bool Trio(List<Card> hand)
    {
        // Same as with pair
        foreach (var card in Counter(hand))
        {
            if (card.Value == 3)
            {
                List<Card> combo = hand.FindAll(a => a.Value == card.Key);
                Console.WriteLine($"Trio: {combo[0].Name} {combo[1].Name} {combo[2].Name}");
                return true;
            }
        }
        return false;
    }
    static private bool Street(List<Card> hand)
    {
        // Here i use universal street check
        if (StreetCheck(hand))
        {
            Console.WriteLine($"Street: {hand[0].Name} {hand[1].Name} {hand[2].Name} {hand[3].Name} {hand[4].Name}");
            return true;
        }
        return false;
    }
    static private bool Flash(List<Card> hand)
    {
        // Here i use universal flash check
        if (FlashCheck(hand))
        {
            Console.WriteLine($"Flash: {hand[0].Name} {hand[1].Name} {hand[2].Name} {hand[3].Name} {hand[4].Name}");
            return true;
        }
        return false;
    }
    static private bool FullHouse(List<Card> hand)
    {
        // Same as with two pairs, but add a check for 3 similar elements
        int i = 0;
        foreach (var card in Counter(hand))
        {
            if (card.Value == 2 || card.Value == 3)
            {
                List<Card> combo = hand.FindAll(a => a.Value == card.Key);
                i++;
                foreach (var card2 in Counter(hand).Skip(i))
                {
                    if (card2.Value == 3 || card2.Value == 2)
                    {
                        combo.AddRange(hand.FindAll(a => a.Value == card2.Key));
                        Console.WriteLine($"Full House: {combo[0].Name} {combo[1].Name} {combo[2].Name} {combo[3].Name} {combo[4].Name}");
                        return true;
                    }
                }
            }
            i++;
        }
        return false;
    }
    static private bool Quadro(List<Card> hand)
    {
        // Same as with pair
        foreach (var card in Counter(hand))
        {
            if (card.Value == 4)
            {
                List<Card> combo = hand.FindAll(a => a.Value == card.Key);
                Console.WriteLine($"Quardo: {combo[0].Name} {combo[1].Name} {combo[2].Name} {combo[3].Name}");
                return true;
            }
        }
        return false;
    }
    static private bool StreetFlash(List<Card> hand)
    {
        // Just check street & flash
        if (StreetCheck(hand) && FlashCheck(hand))
        {
            Console.WriteLine($"Street Flash: {hand[0].Name} {hand[1].Name} {hand[2].Name} {hand[3].Name} {hand[4].Name}");
            return true;
        }
        return false;
    }
    static private bool RoyalFlash(List<Card> hand)
    {
        // Just check street & flash and summarize all cards value for determining if they are royal street.
        if (StreetCheck(hand) && FlashCheck(hand) && hand.Sum(card => card.Value) == 60)
        {
            Console.WriteLine($"Royal Flash: {hand[0].Name} {hand[1].Name} {hand[2].Name} {hand[3].Name} {hand[4].Name}");
            return true;
        }
        return false;
    }

    static private bool FlashCheck(List<Card> hand)
    {
        // Easy solution, if set have 5 elements they are all diferent, so can have similar suit
        HashSet<int> set = new HashSet<int>();
        foreach (Card card in hand)
        {
            set.Add(card.Value);
        }
        // And all that remains is to check whether all cards have the same suit values
        if (set.Count == 5
            && hand[0].Suit == hand[1].Suit
            && hand[1].Suit == hand[2].Suit
            && hand[2].Suit == hand[3].Suit
            && hand[3].Suit == hand[4].Suit)
        {
            return true;
        }
        return false;
    }
    static private bool StreetCheck(List<Card> hand)
    {
        // Easy solution, if we check difference between elements in sorted array and it's 1, so it's sequence of numbers for street
        hand.Sort((card1, card2) => card1.Value.CompareTo(card2.Value));
        if (hand[4].Value - hand[3].Value == 1
            && hand[3].Value - hand[2].Value == 1
            && hand[2].Value - hand[1].Value == 1
            && hand[1].Value - hand[0].Value == 1)
        {
            return true;
        }
        return false;
    }
}