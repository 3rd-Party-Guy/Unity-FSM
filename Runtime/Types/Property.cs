using System;
using ThirdPartyGuy.Collections;

namespace ThirdPartyGuy.FSM
{
    public class Property<T>
    {
        Key key;
        Blackboard context;
        string randID;

        public Property(Blackboard blackboard, T initial = default)
        {
            randID = Guid.NewGuid().ToString();
            context = blackboard;
            key = context.GetOrRegisterKey(randID);
            context.Set(key, initial);
        }
        
        public T Value
        {
            get
            {
                if (context.TryGet<T>(key, out var result))
                {
                    return result;
                }

                return default(T);
            }
            set
            {
                context.Set(key, value);
            }
        }
    }
}
