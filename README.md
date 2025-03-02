Simple card mvc using Zenject

## How to use
Just open the scene `SampleScene` (only one in project) and play it.
You can increment the value of the card by clicking on corresponding buttons.
There is create new deck button that will create a new deck.
There is also save/load deck buttons, however it's saved into a single file.
Didn't have time to implement multiple files saving/loading.

Deck generation is configurable via serialized fields in either `SimpleMainInstaller` or `DeckInstaller`.

## Barebones version
Without external frameworks on main branch commit SHA: 1477d937b5bbcb7a3081e4471874ca97f2778a2f

## External frameworks
- Zenject
- Nuget package manager for Unity
- Newtonsoft.Json

As you can see without zenject I had similar approach to the problem. Some form of installer and passing dependencies to the classes.
Used Interfaces for the classes to be able to swap them easily. IE `IDeckProviderService`.
Also started with built-in UnityJsonUtility for serialization/deserialization.
But it was awful as always. Didn't want to expand model classes too much.
But maybe I should have and use following pattern:

```csharp
public class Model
{
    prviate string m_name;
    
    public string Name
    {
        get
        {
            return m_name;
        }
        set
        {
            if(m_name != value)
            {
                m_name = value;
                OnNameChanged.Invoke(value);
            }
        }
            
    };
    
    public Event<string> OnNameChanged { get; } = new Event<string>();
}
```

That way it should work with UnityJsonUtility, but I ain't going to bet a penny on it. Just wanted to reiterate on how bad UnityJsonUtility is IMO for the last time.
In hindsight, I should have spent more time during week do think stuff through and ask questions how exactly would you like to see it implemented.
But here we are on sunday evening (well at least I am).

Frankly I would like to use https://github.com/Cysharp/R3 for data binding, but I didn't want to push it too far with external libraries.

Installed nuget package manager for Unity to be able to install Newtonsoft.Json package. In this case it's total overkill, but I like it.
There's a lot of useful nuget packages that can be used in Unity and with this package manager it's easy to install them.
Of course some nugets won't work with Unity, but it's easy to check it out. Used it successfully in other projects for example for data export to `.xlsx` files.

Furthermore, I really like Zenject in tandem with Odin Inspector. It's a great combo for Unity projects.
Especially when you modify zenject a bit to inherit odin's `SerializedXYZ` classes instead of unity's regular ones.
But if adding some external libraries was hesitating on my part, then using paid Odin would a bit more than bit too much.

Left some additional comments in the code.

## Known issues
Zenject is adding a project context by itself, but empty SceneContext is added by me.
I'm using `GameObjectContext` for `DeckView` to keep it self-contained, and unfortunately it's not working without `SceneContext`.
This is not a huge issue since in a real project there would be a need for a proper `SceneContext` anyway.

## Time spent
Well, two working days, with proper Unity version download, some breaks, some chores, writing this readme (where writing is not my strong suit) and a bit of procrastination due to overthinking the problem of what to do and what not to do.

## PS
I'm hoping that using git was not a problem. Since I was asked to send a zip file I'm going to send it as well.
Two zips actually, one with the barebones version and one with the external frameworks as well as link to the git repository.