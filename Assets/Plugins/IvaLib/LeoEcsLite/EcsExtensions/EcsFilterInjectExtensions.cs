using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions
{
    public static class EcsFilterInjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1>(this EcsFilterInject<Inc<T1>> filter, int entity)
            where T1 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2>(this EcsFilterInject<Inc<T1, T2>> filter, int entity)
            where T1 : struct
            where T2 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2>(this EcsFilterInject<Inc<T1, T2>> filter, int entity)
            where T1 : struct
            where T2 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3>(this EcsFilterInject<Inc<T1, T2, T3>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3>(this EcsFilterInject<Inc<T1, T2, T3>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3>(this EcsFilterInject<Inc<T1, T2, T3>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4>(this EcsFilterInject<Inc<T1, T2, T3, T4>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4>(this EcsFilterInject<Inc<T1, T2, T3, T4>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4>(this EcsFilterInject<Inc<T1, T2, T3, T4>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4>(this EcsFilterInject<Inc<T1, T2, T3, T4>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4, T5>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4, T5>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4, T5>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4, T5>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T5 Get5<T1, T2, T3, T4, T5>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
        {
            return ref filter.Pools.Inc5.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4, T5, T6>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4, T5, T6>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4, T5, T6>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4, T5, T6>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T5 Get5<T1, T2, T3, T4, T5, T6>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
        {
            return ref filter.Pools.Inc5.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T6 Get6<T1, T2, T3, T4, T5,T6>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
        {
            return ref filter.Pools.Inc6.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetFirstEntity<T1>(this EcsFilterInject<Inc<T1>> filter, out int entity)
            where T1 : struct
        {
            var enumerator = filter.Value.GetEnumerator();
            if (enumerator.MoveNext())
            {
                entity = enumerator.Current;
                return true;
            }

            entity = -1;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetFirstEntity<T1, T2>(this EcsFilterInject<Inc<T1, T2>> filter, out int entity)
            where T1 : struct
            where T2 : struct
        {
            var enumerator = filter.Value.GetEnumerator();
            if (enumerator.MoveNext())
            {
                entity = enumerator.Current;
                return true;
            }

            entity = -1;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetFirstEntity<T1, T2, T3>(this EcsFilterInject<Inc<T1, T2, T3>> filter, out int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var enumerator = filter.Value.GetEnumerator();
            if (enumerator.MoveNext())
            {
                entity = enumerator.Current;
                return true;
            }

            entity = -1;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetFirstEntity<T1, T2, T3, T4>(this EcsFilterInject<Inc<T1, T2, T3, T4>> filter, out int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
        {
            var enumerator = filter.Value.GetEnumerator();
            if (enumerator.MoveNext())
            {
                entity = enumerator.Current;
                return true;
            }

            entity = -1;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetFirstEntity<T1, T2, T3, T4, T5>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>> filter, out int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
        {
            var enumerator = filter.Value.GetEnumerator();
            if (enumerator.MoveNext())
            {
                entity = enumerator.Current;
                return true;
            }

            entity = -1;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetFirstEntity<T1, T2, T3, T4, T5, T6>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>> filter, out int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
        {
            var enumerator = filter.Value.GetEnumerator();
            if (enumerator.MoveNext())
            {
                entity = enumerator.Current;
                return true;
            }

            entity = -1;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, Texc1>(this EcsFilterInject<Inc<T1>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, Texc1>(this EcsFilterInject<Inc<T1, T2>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, Texc1>(this EcsFilterInject<Inc<T1, T2>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, Texc1>(this EcsFilterInject<Inc<T1, T2, T3>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, Texc1>(this EcsFilterInject<Inc<T1, T2, T3>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, Texc1>(this EcsFilterInject<Inc<T1, T2, T3>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4, T5, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4, T5, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4, T5, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4, T5, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T5 Get5<T1, T2, T3, T4, T5, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc5.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4, T5, T6, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4, T5, T6, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4, T5, T6, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4, T5, T6, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T5 Get5<T1, T2, T3, T4, T5, T6, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc5.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T6 Get6<T1, T2, T3, T4, T5, T6, Texc1>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
        {
            return ref filter.Pools.Inc6.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, Texc1, Texc2>(this EcsFilterInject<Inc<T1>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        public static ref T1 Get1<T1, T2, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4, T5, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4, T5, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4, T5, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4, T5, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T5 Get5<T1, T2, T3, T4, T5, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc5.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T1 Get1<T1, T2, T3, T4, T5, T6, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc1.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T2 Get2<T1, T2, T3, T4, T5, T6, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc2.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T3 Get3<T1, T2, T3, T4, T5, T6, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc3.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T4 Get4<T1, T2, T3, T4, T5, T6, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc4.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T5 Get5<T1, T2, T3, T4, T5, T6, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc5.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T6 Get6<T1, T2, T3, T4, T5, T6, Texc1, Texc2>(this EcsFilterInject<Inc<T1, T2, T3, T4, T5, T6>, Exc<Texc1, Texc2>> filter, int entity)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct
            where T6 : struct
            where Texc1 : struct
            where Texc2 : struct
        {
            return ref filter.Pools.Inc6.Get(entity);
        }
    }
}