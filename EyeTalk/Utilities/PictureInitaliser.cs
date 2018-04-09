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
        public Picture pizza = new Picture("a pizza", false, "pack://application:,,,/Images/pizza.png", 0);
        public Picture hotdog = new Picture("a hotdog", false, "pack://application:,,,/Images/hotdog.png", 0);
        public Picture apple = new Picture("an apple", false, "pack://application:,,,/Images/apple.png", 0);
        public Picture banana = new Picture("a banana", false, "pack://application:,,,/Images/banana.png", 0);

        public Picture eggs = new Picture("eggs", false, "pack://application:,,,/Images/eggs.png", 0);
        public Picture burger = new Picture("a burger", false, "pack://application:,,,/Images/burger.png", 0);
        public Picture chicken = new Picture("chicken", false, "pack://application:,,,/Images/chicken.png", 0);
        public Picture chips = new Picture("chips", false, "pack://application:,,,/Images/chips.png", 0);

        //drinks

        public Picture water = new Picture("water", false, "pack://application:,,,/Images/water.png", 0);
        public Picture tea = new Picture("tea", false, "pack://application:,,,/Images/tea.png", 0);
        public Picture coffee = new Picture("coffee", false, "pack://application:,,,/Images/coffee.png", 0);
        public Picture juice = new Picture("juice", false, "pack://application:,,,/Images/juice.png", 0);

        public Picture beer = new Picture("beer", false, "pack://application:,,,/Images/beer.png", 0);
        public Picture wine = new Picture("wine", false, "pack://application:,,,/Images/wine.png", 0);
        public Picture cocktail = new Picture("a cocktail", false, "pack://application:,,,/Images/cocktail.png", 0);

        //emotions
        public Picture sad = new Picture("I am sad", false, "pack://application:,,,/Images/sad.png", 0);
        public Picture happy = new Picture("I am happy", false, "pack://application:,,,/Images/happy.png", 0);
        public Picture love = new Picture("I love", false, "pack://application:,,,/Images/love.png", 0);
        public Picture angry = new Picture("I am angry", false, "pack://application:,,,/Images/angry.png", 0);

        //Feelings
        public Picture hungry = new Picture("I feel hungry", false, "pack://application:,,,/Images/hungry.png", 0);
        public Picture sleepy = new Picture("I feel sleepy", false, "pack://application:,,,/Images/sleepy.png", 0);
        public Picture uncomfortable = new Picture("I feel uncomfortable", false, "pack://application:,,,/Images/uncomfortable.png", 0);
        public Picture pain = new Picture("I am in pain", false, "pack://application:,,,/Images/sick.png", 0);

    
        //Greetings
        public Picture hello = new Picture("hello", false, "pack://application:,,,/Images/hello.png", 0);
        public Picture goodbye = new Picture("goodbye", false, "pack://application:,,,/Images/goodbye.png", 0);

        // replies
        public Picture yes = new Picture("Yes", false, "pack://application:,,,/Images/yes.png", 0);
        public Picture no = new Picture("No", false, "pack://application:,,,/Images/no.png", 0);
        public Picture idontknow = new Picture("I don't know", false, "pack://application:,,,/Images/idontknow.png", 0);

        //Action Words
        public Picture iwant = new Picture("I want", false, "pack://application:,,,/Images/iwant.png", 0);
        public Picture idontwant = new Picture("I don't want", false, "pack://application:,,,/Images/dontwant.png", 0);
        public Picture help = new Picture("I need help with", false, "pack://application:,,,/Images/wow.png", 0);


        //Social
        public Picture thankyou = new Picture("Thank you", false, "pack://application:,,,/Images/thankyou.png", 0);
        public Picture please = new Picture("Please", false, "pack://application:,,,/Images/please.png", 0);
        public Picture excuseme = new Picture("Excuse me", false, "pack://application:,,,/Images/excuseme.png", 0);

        //Time
        public Picture fivemins = new Picture("in 5 minutes", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture thirtymins = new Picture("in 30 minutes", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture onehour = new Picture("in 1 hour", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture fewhours = new Picture("in a few hours", false, "pack://application:,,,/Images/hour.png", 0);

        //When?
        public Picture now = new Picture("Now", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture later = new Picture("Later", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture today = new Picture("Today", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture tomorrow = new Picture("Tomorrow", false, "pack://application:,,,/Images/hour.png", 0);

        //Colours
        public Picture red = new Picture("Red", false, "pack://application:,,,/Images/red.png", 0);
        public Picture blue = new Picture("Blue", false, "pack://application:,,,/Images/blue.png", 0);
        public Picture green = new Picture("Green", false, "pack://application:,,,/Images/green.png", 0);
        public Picture yellow = new Picture("Yellow", false, "pack://application:,,,/Images/yellow.png", 0);

        //Animals
        public Picture own = new Picture("I own a", false, "pack://application:,,,/Images/want.png", 0);
        public Picture dog = new Picture("dog", false, "pack://application:,,,/Images/dog.png", 0);
        public Picture cat = new Picture("cat", false, "pack://application:,,,/Images/cat.png", 0);
        public Picture called = new Picture("called", false, "pack://application:,,,/Images/want.png", 0);

        //people
        public Picture man = new Picture("man", false, "pack://application:,,,/Images/man.png", 0);
        public Picture woman = new Picture("woman", false, "pack://application:,,,/Images/woman.png", 0);
        public Picture girl = new Picture("girl", false, "pack://application:,,,/Images/girl.png", 0);
        public Picture boy = new Picture("boy", false, "pack://application:,,,/Images/boy.png", 0);

        //family
        public Picture brother = new Picture("brother", false, "pack://application:,,,/Images/boy.png", 0);
        public Picture sister = new Picture("sister", false, "pack://application:,,,/Images/girl.png", 0);
        public Picture mum = new Picture("mum", false, "pack://application:,,,/Images/woman.png", 0);
        public Picture dad = new Picture("dad", false, "pack://application:,,,/Images/man.png", 0);

        //Personal Care
        public Picture bath = new Picture("a bath", false, "pack://application:,,,/Images/bath.png", 0);
        public Picture washHands  = new Picture("to wash my hands", false, "pack://application:,,,/Images/washHands.png", 0);
        public Picture shower = new Picture("a shower", false, "pack://application:,,,/Images/shower.png", 0);
        public Picture toilet = new Picture("to go to the toilet", false, "pack://application:,,,/Images/toilet.png", 0);

        //Carers
        public Picture ineed = new Picture("I need", false, "pack://application:,,,/Images/iwant.png", 0);
        public Picture teacher = new Picture("a teacher", false, "pack://application:,,,/Images/teacher.png", 0);
        public Picture doctor = new Picture("a doctor", false, "pack://application:,,,/Images/doctor.png", 0);
        public Picture nurse = new Picture("a nurse", false, "pack://application:,,,/Images/nurse.png", 0);

        //Kitchen Items
        public Picture spoon = new Picture("a spoon", false, "pack://application:,,,/Images/spoon.png", 0);
        public Picture cup = new Picture("a cup", false, "pack://application:,,,/Images/cup.png", 0);
        public Picture plate = new Picture("a plate", false, "pack://application:,,,/Images/plate.png", 0);
        public Picture knifeAndFork = new Picture("a knife and fork", false, "pack://application:,,,/Images/knifeAndFork.png", 0);

        //entertainment Items
        public Picture television = new Picture("to watch television", false, "pack://application:,,,/Images/television.png", 0);
        public Picture radio = new Picture("to listen to radio", false, "pack://application:,,,/Images/radio.png", 0);
        public Picture book = new Picture("to read", false, "pack://application:,,,/Images/book.png", 0);
        public Picture games = new Picture("to play games", false, "pack://application:,,,/Images/games.png", 0);

        public Dictionary<String, List<List<Picture>>> categories;

        //pages
        public List<List<Picture>> foodPages;
        public List<List<Picture>> drinkPages;
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
        public List<List<Picture>> entertainmentPages;
        public List<List<Picture>> kitchenPages;
        public List<List<Picture>> carersPages;
        public List<List<Picture>> personalCarePages;
        public List<List<Picture>> familyPages;


        public List<Picture> family1;
        public List<Picture> personalCare1;
        public List<Picture> carers1;
        public List<Picture> kitchen1;
        public List<Picture> entertainment1;


        public List<Picture> food1;
        public List<Picture> food2;

        public List<Picture> drink1;
        public List<Picture> drink2;


        public List<Picture> emotions1;
        public List<Picture> feelings1;
        public List<Picture> replies1;
        public List<Picture> actionwords1;
        public List<Picture> greetings1;

        public List<Picture> animals1;
        public List<Picture> time2;

        public List<Picture> colours1;

        public List<Picture> time1;


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
            food1 = new List<Picture>() { burger, chips, pizza, eggs };
            food2 = new List<Picture>() {chicken , hotdog, apple, banana };

            drink1 = new List<Picture>() {water, juice, tea, coffee };
            drink2 = new List<Picture>() {beer, wine, cocktail };
            emotions1 = new List<Picture>() { happy, sad, angry, love };

            feelings1 = new List<Picture>() { sleepy, hungry, uncomfortable, pain };

            replies1 = new List<Picture>() { yes, no, idontknow, thankyou };

            actionwords1 = new List<Picture>() {iwant, idontwant};

            greetings1 = new List<Picture>() { hello, goodbye };
   
            animals1 = new List<Picture>() {own, dog, cat };

            colours1 = new List<Picture>() {red, blue, yellow, green };

            time1 = new List<Picture>() { fivemins, thirtymins, onehour, fewhours};
            time2 = new List<Picture>() { now, later, today, tomorrow };

            carers1 = new List<Picture>() { ineed, doctor, teacher, nurse };
            kitchen1 = new List<Picture>() { knifeAndFork, spoon, cup, plate };
            personalCare1 = new List<Picture>() { shower, bath, washHands, toilet };
            entertainment1 = new List<Picture>() { television, book, radio, games };
            family1 = new List<Picture>() { mum, dad, brother, sister };




            custom1 = new List<Picture>() { };
            mostused = new List<Picture>() { };

        }

        public void GenerateCategoryPages()
        {
            foodPages = new List<List<Picture>>() { food1, food2 };
            drinkPages = new List<List<Picture>>() { drink1, drink2 };
            emotionPages = new List<List<Picture>>() { emotions1 };
            actionPages = new List<List<Picture>>() { actionwords1 };
            greetingsPages = new List<List<Picture>>() { greetings1 };
            replyPages = new List<List<Picture>>() { replies1 };
            customPages = new List<List<Picture>>() { custom1 };
            mostUsedPages = new List<List<Picture>>() { mostused };
            timePages = new List<List<Picture>>() { time1, time2 };
            colourPages = new List<List<Picture>>() { colours1 };
            animalPages = new List<List<Picture>>() { animals1 };
            feelingPages = new List<List<Picture>>() { feelings1 };
            carersPages = new List<List<Picture>>() { carers1 };
            kitchenPages = new List<List<Picture>>() { kitchen1 };
            personalCarePages = new List<List<Picture>>() { personalCare1 };
            entertainmentPages = new List<List<Picture>>() { entertainment1 };
            familyPages = new List<List<Picture>>() { family1 };

        }

        public void GenerateCategories()
        {
            categories = new Dictionary<string, List<List<Picture>>>(){
            {"Most Used", mostUsedPages },
            {"Actions", actionPages },
            {"Replies", replyPages },
            {"Foods", foodPages },
            {"Drinks", drinkPages },
            {"Greetings", greetingsPages },
            {"Feelings", feelingPages },          
            {"Emotions", emotionPages },
            {"Colours", colourPages },
            {"Animals", animalPages },
            {"Times", timePages },
            {"Carers", carersPages },
            {"Kitchen", kitchenPages },
            {"Personal Care", personalCarePages },
            {"Entertainment", entertainmentPages },
            {"Family", familyPages },
            {"Custom", customPages },
            };
    }
    }
}
