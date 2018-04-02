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
        //pictures
        public Picture pizza = new Picture("Pizza", false, "pack://application:,,,/Images/pizza.png", 0);
        public Picture hotdog = new Picture("Hotdog", false, "pack://application:,,,/Images/hotdog.png", 0);
        public Picture apple = new Picture("Apple", false, "pack://application:,,,/Images/apple.png", 0);
        public Picture banana = new Picture("Banana", false, "pack://application:,,,/Images/banana.png", 0);
        public Picture sad = new Picture("Sad", false, "pack://application:,,,/Images/sad.png", 0);
        public Picture happy = new Picture("Happy", false, "pack://application:,,,/Images/happy.png", 0);
        public Picture ecstatic = new Picture("Ecstatic", false, "pack://application:,,,/Images/ecstatic.png", 0);
        public Picture awkward = new Picture("Awkward", false, "pack://application:,,,/Images/awkward.png", 0);
        public Picture angry = new Picture("Angry", false, "pack://application:,,,/Images/angry.png", 0);
        public Picture funny = new Picture("Funny", false, "pack://application:,,,/Images/funny.png", 0);
        public Picture hilarious = new Picture("Hilarious", false, "pack://application:,,,/Images/hilarious.png", 0);
        public Picture love = new Picture("Love", false, "pack://application:,,,/Images/love.png", 0);
        public Picture smug = new Picture("Smug", false, "pack://application:,,,/Images/smug.png", 0);
        public Picture wow = new Picture("Wow", false, "pack://application:,,,/Images/wow.png", 0);
        public Picture washhands = new Picture("Wash Hands", false, "pack://application:,,,/Images/washhands.png", 0);
        public Picture toilet = new Picture("Toilet", false, "pack://application:,,,/Images/toilet.png", 0);
        public Picture shower = new Picture("Shower", false, "pack://application:,,,/Images/shower.png", 0);
        public Picture iwant = new Picture("I want", false, "pack://application:,,,/Images/want.png", 0);
        public Picture idontknow = new Picture("I don't know", false, "pack://application:,,,/Images/idontknow.png", 0);

        //subject
        public Picture i = new Picture("I", false, "pack://application:,,,/Images/want.png", 0);
        public Picture you = new Picture("you", false, "pack://application:,,,/Images/want.png", 0);
        public Picture he = new Picture("he", false, "pack://application:,,,/Images/want.png", 0);
        public Picture she = new Picture("she", false, "pack://application:,,,/Images/want.png", 0);
        public Picture it = new Picture("it", false, "pack://application:,,,/Images/want.png", 0);
        public Picture we = new Picture("we", false, "pack://application:,,,/Images/want.png", 0);
        public Picture they = new Picture("they", false, "pack://application:,,,/Images/want.png", 0);

        //object
        public Picture me = new Picture("me", false, "pack://application:,,,/Images/want.png", 0);
        public Picture him = new Picture("him", false, "pack://application:,,,/Images/want.png", 0);
        public Picture her = new Picture("her", false, "pack://application:,,,/Images/want.png", 0);
        public Picture us = new Picture("us", false, "pack://application:,,,/Images/want.png", 0);
        public Picture them = new Picture("them", false, "pack://application:,,,/Images/want.png", 0);

        //adjective
        public Picture my = new Picture("my", false, "pack://application:,,,/Images/want.png", 0);
        public Picture your = new Picture("your", false, "pack://application:,,,/Images/want.png", 0);
        public Picture his = new Picture("his", false, "pack://application:,,,/Images/want.png", 0);
        public Picture its = new Picture("its", false, "pack://application:,,,/Images/want.png", 0);
        public Picture our = new Picture("our", false, "pack://application:,,,/Images/want.png", 0);
        public Picture their = new Picture("their", false, "pack://application:,,,/Images/want.png", 0);


        //possesive
        public Picture mine = new Picture("mine", false, "pack://application:,,,/Images/want.png", 0);
        public Picture yours = new Picture("yours", false, "pack://application:,,,/Images/want.png", 0);
        public Picture ours = new Picture("ours", false, "pack://application:,,,/Images/want.png", 0);
        public Picture theirs = new Picture("theirs", false, "pack://application:,,,/Images/want.png", 0);

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


        public SortedList<String, List<List<Picture>>> categories;


        //pages
        public List<List<Picture>> foodPages;
        public List<List<Picture>> emotionPages;
        public List<List<Picture>> bathroomPages;
        public List<List<Picture>> verbPages;
        public List<List<Picture>> replyPages;
        public List<List<Picture>> customPages;
        public List<List<Picture>> mostUsedPages;
        public List<List<Picture>> conjunctionPages;
        public List<List<Picture>> subjectPronouns;
        public List<List<Picture>> objectPronouns;
        public List<List<Picture>> adjectivePronouns;
        public List<List<Picture>> possesivePronouns;


        //page
        public List<Picture> food1;
        public List<Picture> food2;
        public List<Picture> emotions1;
        public List<Picture> emotions2;
        public List<Picture> emotions3;
        public List<Picture> bathroom1;
        public List<Picture> verb1;
        public List<Picture> reply1;

        public List<Picture> conjunction1;
        public List<Picture> conjunction2;
        public List<Picture> conjunction3;

        public List<Picture> subjectPronoun1;
        public List<Picture> subjectPronoun2;

        public List<Picture> objectPronoun1;
        public List<Picture> objectPronoun2;

        public List<Picture> adjectivePronoun1;
        public List<Picture> adjectivePronoun2;

        public List<Picture> possesivePronoun1;
        public List<Picture> possesivePronoun2;

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
            food2 = new List<Picture>() { };
            emotions1 = new List<Picture>() { happy, sad, angry, love };
            emotions2 = new List<Picture>() { ecstatic, awkward, funny, hilarious };
            emotions3 = new List<Picture>() { wow, smug };
            bathroom1 = new List<Picture>() { washhands, toilet, shower };
            verb1 = new List<Picture>() { iwant };
            reply1 = new List<Picture>() { idontknow };
            custom1 = new List<Picture>() { };
            mostused = new List<Picture>() { };
            conjunction1 = new List<Picture>() {and, although, _as, because };
            conjunction2 = new List<Picture>() {but, _if, or, where };
            conjunction3 = new List<Picture>() {before, after, until, since };

            subjectPronoun1 = new List<Picture>() {i, you,he,she };
            subjectPronoun2 = new List<Picture>() { it,we,they};

            objectPronoun1 = new List<Picture>() { me, him, her, us};
            objectPronoun2 = new List<Picture>() { them };

            adjectivePronoun1 = new List<Picture>() { my, your, his, its };
            adjectivePronoun2 = new List<Picture>() { our, their};

            possesivePronoun1 = new List<Picture>() { mine, yours, its, ours };
            possesivePronoun2 = new List<Picture>() { theirs };

        }

        public void GenerateCategoryPages()
        {
         foodPages = new List<List<Picture>>() { food1 };
         emotionPages = new List<List<Picture>>() { emotions1, emotions2, emotions3 };
         bathroomPages = new List<List<Picture>>() { bathroom1 };
         verbPages = new List<List<Picture>>() { verb1 };
         replyPages = new List<List<Picture>>() { reply1 };
         customPages = new List<List<Picture>>() { custom1 };
         mostUsedPages = new List<List<Picture>>() { mostused };
         conjunctionPages = new List<List<Picture>>() { conjunction1, conjunction2, conjunction3 };
         subjectPronouns = new List<List<Picture>>() { subjectPronoun1, subjectPronoun2 };
         objectPronouns = new List<List<Picture>>() { objectPronoun1, objectPronoun2 };
         adjectivePronouns = new List<List<Picture>>() { adjectivePronoun1, adjectivePronoun2 };
         possesivePronouns = new List<List<Picture>>() { possesivePronoun1, possesivePronoun2 };
        }

        public void GenerateCategories()
        {
            categories = new SortedList<string, List<List<Picture>>>(){
            {"Most Used", mostUsedPages },        
            {"Emotions", emotionPages },
            {"Bathroom", bathroomPages },
            {"Verbs", verbPages },
            {"Replies", replyPages },
            {"Custom", customPages },
            {"Conjunctions", conjunctionPages },
            {"Subject Pronouns", subjectPronouns },
            {"Object Pronouns", objectPronouns },
            { "Adjective Pronouns", adjectivePronouns },
            { "Possesive Pronouns", possesivePronouns },
             {"Foods", foodPages },
            };
    }
    }
}
