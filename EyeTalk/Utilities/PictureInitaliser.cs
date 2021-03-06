﻿using EyeTalk.Objects;
using System.Collections.Generic;


namespace EyeTalk.Utilities
{
    class PictureInitialiser
    {
        //Categories list
        public List< List<List<Picture>>> categories;
        

        //Categories
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
        public List<List<Picture>> bathroomPages;
        public List<List<Picture>> familyPages;
        public List<List<Picture>> connectingWordsPages;
        public List<List<Picture>> dressingPages;
        public List<List<Picture>> personalCarePages;


        //Pages
        public List<Picture> family1;
        public List<Picture> family2;

        public List<Picture> bathroom1;

        public List<Picture> carers1;
        public List<Picture> carers2;

        public List<Picture> kitchen1;

        public List<Picture> entertainment1;
        public List<Picture> entertainment2;

        public List<Picture> connectingWords1;

        public List<Picture> dressing1;

        public List<Picture> food1;
        public List<Picture> food2;

        public List<Picture> drink1;
        public List<Picture> drink2;

        public List<Picture> emotions1;

        public List<Picture> personalCare1;

        public List<Picture> feelings1;

        public List<Picture> replies1;

        public List<Picture> actionwords1;
        public List<Picture> actionwords2;

        public List<Picture> greetings1;

        public List<Picture> animals1;
        public List<Picture> animals2;

        public List<Picture> colours1;

        public List<Picture> time1;
        public List<Picture> time2;

        public List<Picture> custom1;
        public List<Picture> mostused;

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

        //Personal Feeling
        public Picture hungry = new Picture("I feel hungry", false, "pack://application:,,,/Images/hungry.png", 0);
        public Picture sleepy = new Picture("I feel sleepy", false, "pack://application:,,,/Images/sleepy.png", 0);
        public Picture uncomfortable = new Picture("I feel uncomfortable", false, "pack://application:,,,/Images/uncomfortable.png", 0);
        public Picture pain = new Picture("I am in pain", false, "pack://application:,,,/Images/sick.png", 0);

        //Greetings
        public Picture hello = new Picture("hello", false, "pack://application:,,,/Images/hello.png", 0);
        public Picture goodbye = new Picture("goodbye", false, "pack://application:,,,/Images/goodbye.png", 0);

        // replies
        public Picture yes = new Picture("yes", false, "pack://application:,,,/Images/like.png", 0);
        public Picture no = new Picture("no", false, "pack://application:,,,/Images/dislike.png", 0);
        public Picture idontknow = new Picture("I don't know", false, "pack://application:,,,/Images/idontknow.png", 0);

        //Action Words
        public Picture iwant = new Picture("I want", false, "pack://application:,,,/Images/iwant.png", 0);
        public Picture idontwant = new Picture("I don't want", false, "pack://application:,,,/Images/dontwant.png", 0);
        public Picture help = new Picture("I need help", false, "pack://application:,,,/Images/help.png", 0);
        public Picture iamfinishedwith = new Picture("I am finished with", false, "pack://application:,,,/Images/finished.png", 0);

        public Picture ilike = new Picture("I like", false, "pack://application:,,,/Images/like.png", 0);
        public Picture idontlike = new Picture("I don't like", false, "pack://application:,,,/Images/dislike.png", 0);
        public Picture imiss = new Picture("I miss", false, "pack://application:,,,/Images/friends.png", 0);

        //Social
        public Picture thankyou = new Picture("thank you", false, "pack://application:,,,/Images/thankyou.png", 0);
        public Picture please = new Picture("please", false, "pack://application:,,,/Images/please.png", 0);
        public Picture excuseme = new Picture("excuse me", false, "pack://application:,,,/Images/excuseme.png", 0);

        //Time
        public Picture fivemins = new Picture("in 5 minutes", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture thirtymins = new Picture("in 30 minutes", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture onehour = new Picture("in 1 hour", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture fewhours = new Picture("in a few hours", false, "pack://application:,,,/Images/hour.png", 0);

        //When?
        public Picture now = new Picture("now", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture later = new Picture("later", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture today = new Picture("today", false, "pack://application:,,,/Images/hour.png", 0);
        public Picture tomorrow = new Picture("tomorrow", false, "pack://application:,,,/Images/hour.png", 0);

        //Colours
        public Picture red = new Picture("red", false, "pack://application:,,,/Images/red.png", 0);
        public Picture blue = new Picture("blue", false, "pack://application:,,,/Images/blue.png", 0);
        public Picture green = new Picture("green", false, "pack://application:,,,/Images/green.png", 0);
        public Picture yellow = new Picture("yellow", false, "pack://application:,,,/Images/yellow.png", 0);

        //Animals
        public Picture own = new Picture("I own a", false, "pack://application:,,,/Images/want.png", 0);
        public Picture feed = new Picture("feed my", false, "pack://application:,,,/Images/feed.png", 0);
        public Picture dog = new Picture("dog", false, "pack://application:,,,/Images/dog.png", 0);
        public Picture cat = new Picture("cat", false, "pack://application:,,,/Images/cat.png", 0);

        public Picture bird = new Picture("bird", false, "pack://application:,,,/Images/bird.png", 0);
        public Picture horse = new Picture("horse", false, "pack://application:,,,/Images/horse.png", 0);
        public Picture fish = new Picture("fish", false, "pack://application:,,,/Images/fish.png", 0);

        //people
        public Picture man = new Picture("man", false, "pack://application:,,,/Images/man.png", 0);
        public Picture woman = new Picture("woman", false, "pack://application:,,,/Images/woman.png", 0);
        public Picture girl = new Picture("girl", false, "pack://application:,,,/Images/girl.png", 0);
        public Picture boy = new Picture("boy", false, "pack://application:,,,/Images/boy.png", 0);
        public Picture friends = new Picture("my friends", false, "pack://application:,,,/Images/friends.png", 0);
        public Picture people = new Picture("people", false, "pack://application:,,,/Images/people.png", 0);

        //family
        public Picture brother = new Picture("my brother", false, "pack://application:,,,/Images/boy.png", 0);
        public Picture sister = new Picture("my sister", false, "pack://application:,,,/Images/girl.png", 0);
        public Picture mum = new Picture("my mum", false, "pack://application:,,,/Images/woman.png", 0);
        public Picture dad = new Picture("my dad", false, "pack://application:,,,/Images/man.png", 0);
        //family
        public Picture uncle = new Picture("my uncle", false, "pack://application:,,,/Images/man.png", 0);
        public Picture auntie = new Picture("my auntie", false, "pack://application:,,,/Images/woman.png", 0);
        public Picture granny = new Picture("my granny", false, "pack://application:,,,/Images/granny.png", 0);
        public Picture granda = new Picture("my granda", false, "pack://application:,,,/Images/granda.png", 0);

        //bathroom
        public Picture bath = new Picture("a bath", false, "pack://application:,,,/Images/bath.png", 0);
        public Picture washHands = new Picture("to wash my hands", false, "pack://application:,,,/Images/washHands.png", 0);
        public Picture shower = new Picture("a shower", false, "pack://application:,,,/Images/shower.png", 0);
        public Picture toilet = new Picture("to go to the toilet", false, "pack://application:,,,/Images/toilet.png", 0);

        //Carers
        public Picture ineed = new Picture("I need", false, "pack://application:,,,/Images/iwant.png", 0);
        public Picture teacher = new Picture("a teacher", false, "pack://application:,,,/Images/teacher.png", 0);
        public Picture doctor = new Picture("a doctor", false, "pack://application:,,,/Images/doctor.png", 0);
        public Picture nurse = new Picture("a nurse", false, "pack://application:,,,/Images/nurse.png", 0);

        public Picture fireman = new Picture("a fireman", false, "pack://application:,,,/Images/woman.png", 0);
        public Picture policeman = new Picture("a policeman", false, "pack://application:,,,/Images/man.png", 0);

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

        public Picture music = new Picture("to listen to music", false, "pack://application:,,,/Images/music.png", 0);
        public Picture outside = new Picture("to go outside", false, "pack://application:,,,/Images/cinema.png", 0);
        public Picture shopping = new Picture("to go shopping", false, "pack://application:,,,/Images/shopping.png", 0);
        public Picture cinema = new Picture("to go the cinema", false, "pack://application:,,,/Images/cinema.png", 0);

        //connecting Items
        public Picture and = new Picture("and", false, "pack://application:,,,/Images/plus.png", 0);
        public Picture or = new Picture("or", false, "pack://application:,,,/Images/plus.png", 0);
        public Picture with = new Picture("with", false, "pack://application:,,,/Images/iwant.png", 0);
        public Picture in_ = new Picture("in", false, "pack://application:,,,/Images/in.png", 0);

        //getting ready Items
        public Picture dressed = new Picture("to get dressed", false, "pack://application:,,,/Images/dressed.png", 0);
        public Picture trousers = new Picture(" to put trousers on", false, "pack://application:,,,/Images/trousers.png", 0);
        public Picture shirt = new Picture("to put shirt on", false, "pack://application:,,,/Images/shirt.png", 0);
        public Picture shoes = new Picture("to put shoes on", false, "pack://application:,,,/Images/shoes.png", 0);

        public Picture deodorant = new Picture("to put deodorant on", false, "pack://application:,,,/Images/deodorant.png", 0);
        public Picture comb = new Picture("to comb my hair", false, "pack://application:,,,/Images/comb.png", 0);
        public Picture cutHair = new Picture("to cut my hair", false, "pack://application:,,,/Images/cuthair.png", 0);
        public Picture blowNose = new Picture("to blow my nose", false, "pack://application:,,,/Images/blownose.png", 0);

        public PictureInitialiser()
        {
            GenerateCategoryPage();
            GenerateCategoryPages();
            GenerateCategories();
        }

        public void GenerateCategoryPage()
        {
            //Adds the pictures to the pages
            food1 = new List<Picture>() { burger, chips, pizza, eggs };
            food2 = new List<Picture>() {chicken , hotdog, apple, banana };

            drink1 = new List<Picture>() {water, juice, tea, coffee };
            drink2 = new List<Picture>() {beer, wine, cocktail };

            emotions1 = new List<Picture>() { happy, sad, angry, love };

            feelings1 = new List<Picture>() { sleepy, hungry, uncomfortable, pain };

            replies1 = new List<Picture>() { yes, no, idontknow, thankyou };

            actionwords1 = new List<Picture>() {iwant, idontwant, ilike, idontlike};
            actionwords2 = new List<Picture>() { imiss, help, iamfinishedwith, idontlike };

            greetings1 = new List<Picture>() { hello, goodbye };
   
            animals1 = new List<Picture>() {feed, own, dog, cat };
            animals2 = new List<Picture>() { horse, fish, bird };

            colours1 = new List<Picture>() {red, blue, yellow, green };

            time1 = new List<Picture>() { fivemins, thirtymins, onehour, fewhours};
            time2 = new List<Picture>() { now, later, today, tomorrow };

            carers1 = new List<Picture>() { ineed, doctor, teacher, nurse };
            carers2 = new List<Picture>() { fireman, policeman };

            kitchen1 = new List<Picture>() { knifeAndFork, spoon, cup, plate };

            bathroom1 = new List<Picture>() { shower, bath, washHands, toilet };

            entertainment1 = new List<Picture>() { television, book, radio, games };
            entertainment2 = new List<Picture>() { music, cinema, outside, shopping };

            family1 = new List<Picture>() { mum, dad, brother, sister };
            family2 = new List<Picture>() { uncle, auntie, granny, granda };

            connectingWords1 = new List<Picture>() { and, or, with,in_};

            dressing1 = new List<Picture>() {dressed, trousers, shirt, shoes };

            personalCare1 = new List<Picture>() { deodorant, comb, cutHair, blowNose };

            custom1 = new List<Picture>() { };

            mostused = new List<Picture>() { };

        }

        public void GenerateCategoryPages()
        {
            //adds the category pages to the categories
            foodPages = new List<List<Picture>>() { food1, food2 };
            drinkPages = new List<List<Picture>>() { drink1, drink2 };
            emotionPages = new List<List<Picture>>() { emotions1 };
            actionPages = new List<List<Picture>>() { actionwords1, actionwords2 };
            greetingsPages = new List<List<Picture>>() { greetings1 };
            replyPages = new List<List<Picture>>() { replies1 };
            customPages = new List<List<Picture>>() { custom1 };
            mostUsedPages = new List<List<Picture>>() { mostused };
            timePages = new List<List<Picture>>() { time1, time2 };
            colourPages = new List<List<Picture>>() { colours1 };
            animalPages = new List<List<Picture>>() { animals1, animals2 };
            feelingPages = new List<List<Picture>>() { feelings1 };
            carersPages = new List<List<Picture>>() { carers1, carers2 };
            kitchenPages = new List<List<Picture>>() { kitchen1 };
            bathroomPages = new List<List<Picture>>() { bathroom1 };
            entertainmentPages = new List<List<Picture>>() { entertainment1, entertainment2 };
            familyPages = new List<List<Picture>>() { family1, family2 };
            connectingWordsPages = new List<List<Picture>>() { connectingWords1 };
            dressingPages = new List<List<Picture>>() { dressing1 };
            personalCarePages = new List<List<Picture>>() { personalCare1 };



        }

        public void GenerateCategories()
        {
            //adds the categories to the category list
            categories = new List<List<List<Picture>>>(){
            
            {actionPages }, //0
            {replyPages }, //1
            {foodPages }, //2
            {drinkPages }, //3
            {greetingsPages }, //4
            {feelingPages },      //5    
            {emotionPages }, //6
            {colourPages }, //7
            {animalPages }, //8
            {timePages }, //9
            {carersPages }, //10
            {kitchenPages }, //11
            {bathroomPages }, //12
            {entertainmentPages }, //13
            {familyPages }, //14
            {customPages }, //15
            {mostUsedPages}, //16
            {connectingWordsPages}, //17
            {dressingPages}, //18
            {personalCarePages}, //19

            };
        }
     }
}
