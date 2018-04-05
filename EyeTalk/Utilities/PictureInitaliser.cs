using EyeTalk.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTalk.Utilities
{
    class PictureInitialiser
    {
        //Food
        public Picture pizza = new Picture("Pizza", false, "pack://application:,,,/Images/pizza.png", 0);
        public Picture hotdog = new Picture("Hotdog", false, "pack://application:,,,/Images/hotdog.png", 0);
        public Picture apple = new Picture("Apple", false, "pack://application:,,,/Images/apple.png", 0);
        public Picture banana = new Picture("Banana", false, "pack://application:,,,/Images/banana.png", 0);

        //emotions
        public Picture sad = new Picture("I am sad", false, "pack://application:,,,/Images/sad.png", 0);
        public Picture happy = new Picture("I am happy", false, "pack://application:,,,/Images/happy.png", 0);
        public Picture love = new Picture("I love", false, "pack://application:,,,/Images/love.png", 0);
        public Picture angry = new Picture("I am angry", false, "pack://application:,,,/Images/angry.png", 0);

        //Feelings
        public Picture awkward = new Picture("Awkward", false, "pack://application:,,,/Images/awkward.png", 0);
        public Picture ecstatic = new Picture("Ecstatic", false, "pack://application:,,,/Images/ecstatic.png", 0);
        public Picture funny = new Picture("Funny", false, "pack://application:,,,/Images/funny.png", 0);
        public Picture hilarious = new Picture("Hilarious", false, "pack://application:,,,/Images/hilarious.png", 0);

    
        //Greetings
        public Picture hello = new Picture("hello", false, "pack://application:,,,/Images/want.png", 0);
        public Picture goodbye = new Picture("goodbye", false, "pack://application:,,,/Images/want.png", 0);
        public Picture seeyoulater = new Picture("See you later", false, "pack://application:,,,/Images/want.png", 0);
        public Picture seeyou = new Picture("Nice to see you", false, "pack://application:,,,/Images/want.png", 0);

        // replies
        public Picture yes = new Picture("Yes", false, "pack://application:,,,/Images/shower.png", 0);
        public Picture no = new Picture("No", false, "pack://application:,,,/Images/want.png", 0);
        public Picture idontknow = new Picture("I don't know", false, "pack://application:,,,/Images/idontknow.png", 0);

        //Actions Words
        public Picture iwant = new Picture("I want", false, "pack://application:,,,/Images/smug.png", 0);
        public Picture idontwant = new Picture("I don't want a", false, "pack://application:,,,/Images/wow.png", 0);
        public Picture iwouldlike = new Picture("I would like", false, "pack://application:,,,/Images/washhands.png", 0);
        public Picture a = new Picture("a ", false, "pack://application:,,,/Images/toilet.png", 0);


        //Social
        public Picture thankyou = new Picture("Thank you", false, "pack://application:,,,/Images/want.png", 0);
        public Picture please = new Picture("Please", false, "pack://application:,,,/Images/want.png", 0);
        public Picture excuseme = new Picture("Excuse me", false, "pack://application:,,,/Images/want.png", 0);

        //Time
        public Picture fivemins = new Picture("in 5 minutes", false, "pack://application:,,,/Images/want.png", 0);
        public Picture thirtymins = new Picture("in 30 minutes", false, "pack://application:,,,/Images/want.png", 0);
        public Picture onehour = new Picture("in 1 hour", false, "pack://application:,,,/Images/want.png", 0);
        public Picture fewhours = new Picture("in a few hours", false, "pack://application:,,,/Images/want.png", 0);

        //When?
        public Picture now = new Picture("Now", false, "pack://application:,,,/Images/want.png", 0);
        public Picture later = new Picture("Later", false, "pack://application:,,,/Images/want.png", 0);
        public Picture today = new Picture("Today", false, "pack://application:,,,/Images/want.png", 0);
        public Picture tomorrow = new Picture("Tomorrow", false, "pack://application:,,,/Images/want.png", 0);

        //Colours
        public Picture red = new Picture("Red", false, "pack://application:,,,/Images/want.png", 0);
        public Picture blue = new Picture("Blue", false, "pack://application:,,,/Images/want.png", 0);
        public Picture green = new Picture("Green", false, "pack://application:,,,/Images/want.png", 0);
        public Picture yellow = new Picture("Yellow", false, "pack://application:,,,/Images/want.png", 0);

        //Animals
        public Picture dog = new Picture("Dog", false, "pack://application:,,,/Images/want.png", 0);
        public Picture cat = new Picture("Cat", false, "pack://application:,,,/Images/want.png", 0);

        //conjunctions
        public Picture and = new Picture("and", false, "pack://application:,,,/Images/want.png", 0);
        public Picture although = new Picture("although", false, "pack://application:,,,/Images/want.png", 0);
        public Picture _as = new Picture("as", false, "pack://application:,,,/Images/want.png", 0);
        public Picture because = new Picture("because", false, "pack://application:,,,/Images/want.png", 0);

        public Picture but = new Picture("but", false, "pack://application:,,,/Images/want.png", 0);
        public Picture _if = new Picture("if", false, "pack://application:,,,/Images/want.png", 0);
        public Picture or = new Picture("or", false, "pack://application:,,,/Images/want.png", 0);
        public Picture where = new Picture("where", false, "pack://application:,,,/Images/want.png", 0);

        public Picture before = new Picture("before", false, "pack://application:,,,/Images/want.png", 0);
        public Picture after = new Picture("after", false, "pack://application:,,,/Images/want.png", 0);
        public Picture until = new Picture("until", false, "pack://application:,,,/Images/want.png", 0);
        public Picture since = new Picture("since", false, "pack://application:,,,/Images/want.png", 0);


        public Dictionary<String, List<List<Picture>>> categories;


        //pages
        public List<List<Picture>> foodPages;
        public List<List<Picture>> emotionPages;
        public List<List<Picture>> actionPages;
        public List<List<Picture>> greetingsPages;
        public List<List<Picture>> replyPages;
        public List<List<Picture>> customPages;
        public List<List<Picture>> mostUsedPages;
        public List<List<Picture>> timePages;
        public List<List<Picture>> colourPages;
        public List<List<Picture>> animalPages;
        public List<List<Picture>> feelingPages;
        public List<List<Picture>> possesivePronouns;


        //page
        public List<Picture> food1;
        public List<Picture> food2;
        public List<Picture> emotions1;
        public List<Picture> feelings1;
        public List<Picture> replies;
        public List<Picture> actionwords;
        public List<Picture> verb1;
        public List<Picture> greetings;

        public List<Picture> social;
        public List<Picture> animals;
        public List<Picture> when;

        public List<Picture> colours;

        public List<Picture> time;


        public List<Picture> custom1;
        public List<Picture> mostused;
   

        public PictureInitialiser()
        {
            GenerateCategoryPage();
            GenerateCategoryPages();
            GenerateCategories();
        }

        public void GenerateCategoryPage()
        {
            food1 = new List<Picture>() { pizza, hotdog, apple, banana };

            emotions1 = new List<Picture>() { happy, sad, angry, love };

            feelings1 = new List<Picture>() { ecstatic, awkward, funny, hilarious };

            replies = new List<Picture>() { yes, no, idontknow };
            actionwords = new List<Picture>() {iwant, idontwant, iwouldlike, a};

            greetings = new List<Picture>() { hello, goodbye, seeyoulater };

     
            animals = new List<Picture>() {dog, cat };
            when = new List<Picture>() {now, later, today, tomorrow };

            colours = new List<Picture>() {red, blue, yellow, green };
            social = new List<Picture>() { thankyou,please,excuseme};

            time = new List<Picture>() { fivemins, thirtymins, onehour, fewhours};

            custom1 = new List<Picture>() { };
            mostused = new List<Picture>() { };

        }

        public void GenerateCategoryPages()
        {
         foodPages = new List<List<Picture>>() { food1 };
         emotionPages = new List<List<Picture>>() { emotions1 };
         actionPages = new List<List<Picture>>() { actionwords };
         greetingsPages = new List<List<Picture>>() { greetings };
         replyPages = new List<List<Picture>>() { replies };
         customPages = new List<List<Picture>>() { custom1 };
         mostUsedPages = new List<List<Picture>>() { mostused };
         timePages = new List<List<Picture>>() { time, when };
         colourPages = new List<List<Picture>>() { colours };
         animalPages = new List<List<Picture>>() { animals };
         feelingPages = new List<List<Picture>>() { feelings1 };
        }

        public void GenerateCategories()
        {
            categories = new Dictionary<string, List<List<Picture>>>(){
            {"Most Used", mostUsedPages },
            {"Action Words", actionPages },
            {"Replies", replyPages },
            {"Greetings", greetingsPages },
            {"Feelings", feelingPages },
            {"Custom", customPages },
            {"Emotions", emotionPages },
            {"Colours", colourPages },
            {"Animals", animalPages },
            { "Times", timePages },
             {"Foods", foodPages },
             

            };
    }
    }
}
