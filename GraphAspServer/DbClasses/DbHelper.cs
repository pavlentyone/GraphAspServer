using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbClasses
{
    public struct WagonStruct{
        int id;
        string number;
    }

    public struct LocomotiveStruct{
        int id;
        string number;
    }
    
    public struct TrainStruct{
        int id;
        string number;

        WagonStruct[] wagons;
        LocomotiveStruct[] locomotives;    
    }

    public enum OperationType : byte
    {
        Nothing = 1,                            // Без операции
        Arrival = 2,                            // Прибытие на станцию
        Departure = 3,                          // Отправление со станции
        StationFollowing = 4,                   // Проследование станции
        LocomotiveUncoupling = 5,               // Отцепка локомотива от поезда
        LocomotiveCoupling = 6,                 // Прицепка локомотива к поезду
        Rearrangement = 7,                      // Перестановка на другой путь
        ContorlPanelPreliminaryInformation = 8, // Предварительная информация на поезд от ПКП
        Maneuver = 9,                           // Маневровое перемещение
        WagonUncoupling = 10,                   // Отцепка вагонов от поезда
        WagonCoupling = 11,                     // Прицепка вагонов к поезду
        EndOfDisbandment = 12,                  // Окончание расформирования 
        ControlPanelReception = 14,             // Прием поезда по ВВ от агента ПКП
        ProcessingReadiness = 15,               // Готовность к обработке
        TrainFormation = 16,                    // Формирование поезда
        CarRefusal = 17,                        // Выкидка вагона из поезда
        EndOfTrainFormation = 18,               // Окончательное формирование поезда
        CancelEndOfTrainFormation = 19,         // Отмена окончательного формирования
        SortingSheet = 20,                      // Расчёт сортировочного листа
        SeamFollowUp = 21,                      // Проследование стыка
        StartOfDisbandment = 22,                // Начало расформирования
        CorrectionOfTheSheet = 23,              // Корректировка сведений о вагонах для натурного листа
        AdjustTrainComposition = 24,            // Корректировка состава поезда
        WideToNarrowGaugeShift = 25,            // Перестановка вагонов с широкой колеи на узкую
        NarrowToWideGaugeShift = 26,            // Перестановка вагонов с узкой колеи на широкую
        TrainConnection = 27,                   // Соединение поездов
        TrainConnectionCanceling = 28,          // Отмена соединения поездов
        BorderControl = 31,                     // Пограничный контроль
        PanelControlDelivery = 32,              // Сдача поезда по ВВ агенту ПКП
        TrainFencing = 33,                      // Ограждение
        TrainFencingRemoval = 34,               // Снятие ограждения
        ChangeOfTrainnumber = 35,               // Изменение номера поезда
        PreliminaryInformationDeletion = 80,    // Удаление предв.информации
        Deletion = 90                           // Удаление поезда с полигона ПР
    }

    public static class PixelUnit
    {
        public const double Pixel = 1.0;
        public const double Inch = 96.0;
        public const double Centimeter = 37.795275590551178;
    }
    public static class DbHelper
    {
        public static readonly int[] wagonOperations =
            { 2, 3, 4, 7, 8, 9, 10, 11, 14, 16, 21, 23, 24, 31, 32, 35/*, 90*/ };

        public static readonly int[] locomotiveOperations =
            { /*1, */2, 3, /*4, */5, 6, /*7, 8,*/ 16, /*21, 35, 90 */};

        // operations i use
        public static readonly int[] trainOperations =
            { 2, 3, 5, 6, 7, 8, 9, 10, 11, 12, 16, 21, 31, 80, 90 };

        public static readonly int[] deathTrainOperations =
            { 3, 8, 12, 80, 90 };

        public static readonly int[] visibleOperations =
            { 2, 3, 7, 9, 10, 11, 12, 16, 31, 90 };

        public static readonly int[] locoStartIds = new int[] { 2 };
        public static readonly int[] locoFinishIds = new int[] { 3 };


        public static readonly Dictionary<string, string> parksEncriptions = new Dictionary<string, string>()
        {
            {"01", "Парк Заречица" },
            {"02", "Северный парк" },
            {"03", "Парк Cортировочный" },
            {"04", "Выставочный парк" },
            {"05", "Обменный парк" },
            {"06", "Перевалочный парк" },

            {"1", "Парк Заречица" },
            {"2", "Северный парк" },
            {"3", "Парк Cортировочный" },
            {"4", "Выставочный парк" },
            {"5", "Обменный парк" },
            {"6", "Перевалочный парк" }
        };

        public static readonly Dictionary<int, string> waysEncriptions = new Dictionary<int, string>()
        {
            {119, "II" },
            {120, "III" }
        };

        public static Railway[] LoadWays(int[] template)
        {
            using (var context = new SqlContext())
            {
                if (!context.Database.CanConnect())
                    return new Railway[0];

                return
                    (from t in template
                     select (from r in context.RailwayNodes.AsNoTracking()
                        where t == r.RailwayId
                        select r).AsEnumerable().FirstOrDefault()).ToArray();
            }
        }

        public static Shift[] LoadShifts(DateTime begining, DateTime end)
        {
            using (var context = new SqlContext())
            {
                if (!context.Database.CanConnect())
                    return new Shift[0];

                return
                    (from s in context.Shifts.Include(s => s.User1).Include(s => s.User2).AsNoTracking()
                    where s.ShiftDate.HasValue && begining.AddHours(-1) <= s.ShiftDate.Value && s.ShiftDate.Value < end
                    orderby s.ShiftDate
                    select s).AsEnumerable().ToArray();
            }
        }
        
        public static int[] 
            LoadIds(SqlContext context, DateTime begining, DateTime end) 
            =>
            (from t in context.Trains.AsNoTracking()
             where t.OperationDate > new DateTime(2021, 1, 1, 0, 0, 0, 0) 
             && t.OperationDate < end 
             && trainOperations.Contains(t.OperationId)
             
             group t by t.TrainId into g
             where 
             g.Where(x => x.OperationDate < begining && x.OperationId == 90)
             .Count() == 0
             select g.Key)
            .ToArray();


        public static Train[] 
            LoadTrains(SqlContext context, DateTime begining, DateTime end, int[] ids) 
            =>
            (from t in context.Trains.Include(t => t.Railway).Include(t => t.Operation).Include(t => t.Carrier).AsNoTracking()
             where 
             trainOperations.Contains(t.OperationId)
             && ids.Contains(t.TrainId)
             && t.OperationDate <= end
             && t.CarrierId != 19
             orderby t.OperationDate
             select t).ToArray();

        public static Train[][] 
            GetStories(Train[] value) 
            =>
            (from v in value
            group v by v.TrainId into g
            select g.ToArray()).ToArray();

        public static Train[][] 
            CutWrongOps(Train[][] stories, int[] ways, DateTime begining) 
            =>
            (from ss in stories
             where ss.Any(x => ways.Contains(x.RailwayId))
             select
            (from s in ss
             //where
             //ways.Contains(s.RailwayId)
             //&& s.OperationDate >= begining
             //|| s.OperationId
             //is (int)OperationType.Rearrangement
             //or (int)OperationType.WagonCoupling
             //or (int)OperationType.WagonUncoupling
             select s)
             .ToArray())
            .Where(x=>x.Count() != 0)
            .ToArray();

        public static Locomotive[] LoadLocomotives(int[] waysIds, DateTime begining, DateTime end)
        {
            var tempBegining = begining;
            var tempBegining2 = begining;
            var tempEnd = end;
            var tempWaysIds = waysIds.ToArray();

            //var filteredBeforeTask = Task.Run(() =>
            //{
            using (var context = new SqlContext())
            {
                // Take all the last operations for each train before current period of time
                var lastBefore =
                    from t in context.Locomotives.AsNoTracking()
                    where t.OperationDate > new DateTime(2021, 1, 1, 0, 0, 0, 0)
                    && t.OperationDate < tempBegining
                    && (locoStartIds.Contains(t.OperationId) || locoFinishIds.Contains(t.OperationId))
                    group t by t.LocomotiveId into g
                    select new
                    {
                        id = g.Key,
                        date = g.Max(t => t.OperationDate)
                    };

                // cut out all the operations where trains has died
                var filteredBeforeTask =
                    (from a in lastBefore.AsNoTracking()
                     join t in context.Locomotives.Include(t => t.Train).Include(t => t.Railway).Include(t => t.Operation).Include(t => t.Carrier).AsNoTracking()
                     on a.id equals t.LocomotiveId
                     where t.OperationDate == a.date && t.OperationId != 3
                     orderby t.OperationDate
                     select t).AsEnumerable();
                //}
                //});

                //var queryTask = Task.Run(() =>
                //{
                //using (var context = new SqlContext())
                //{
                var queryTask =
                (from l in context.Locomotives.Include(l => l.Railway).Include(l => l.Train).Include(l => l.Operation).Include(t => t.Carrier).AsNoTracking()
                 where tempBegining2 <= l.OperationDate && l.OperationDate <= tempEnd
                 //&& locIds.Contains(l.LocomotiveId)
                 && tempWaysIds.Contains(l.RailwayId)
                 && (locoStartIds.Contains(l.OperationId) || locoFinishIds.Contains(l.OperationId))
                 orderby l.OperationDate
                 select l).AsEnumerable();
                // }
                //});

                //Task.WaitAll(filteredBeforeTask, queryTask);

                return
                    filteredBeforeTask
                    .Union(queryTask)
                    .ToArray();
            }
        }

        public static Wagon[] LoadWagons(SqlContext context, int[] ids)
            =>
            (from w in context.Wagons.AsNoTracking()
            where
                w.OperationDate > new DateTime(2021, 1, 1)
                && ids.Contains(w.TrainId)
                && trainOperations.Contains(w.OperationId)
                && visibleOperations.Contains(w.OperationId)
            select w)
            .ToArray();

        public static void SetWagons(Train[][] stories, Wagon[] wagons)
        {
            foreach (var story in stories)
                foreach (var train in story)
                    train.wagons =
                        (from w in wagons
                         where
                            w.TrainId == train.TrainId
                            && w.OperationDate == train.OperationDate
                            && w.OperationId == train.OperationId
                         orderby w.WagonId
                         select w)
                         .ToArray();

            foreach (var story in stories)
                for (var i = 0; i < story.Length; i++)
                    if (story[i].wagons is null)
                        story[i].wagons = InitGetWagon(story, i);
        }

        public static Locomotive[] LoadLocomotives(SqlContext context, int[] ids) 
            =>
            (from w in context.Locomotives.AsNoTracking()
            where 
                w.OperationDate > new DateTime(2021, 1, 1)
                && w.TrainId != null
                && (ids.Contains((int)w.TrainId) || w.TrainId == 0)
                && trainOperations.Contains(w.OperationId)
            select w)
            .ToArray();

        public static void SetLocomotives(Train[][] stories, Locomotive[] locos)
        {
            foreach (var story in stories)
                foreach (var train in story)
                    train.locomotives =
                        (from w in locos
                         where
                             (w.TrainId == train.TrainId || w.TrainId == 0)
                             && w.OperationDate == train.OperationDate
                             && w.OperationId == train.OperationId
                         orderby w.LocomotiveId
                         select w)
                         .ToArray();
        }

        public static Wagon[] InitGetWagon(Train[] chain, int i)
        {
            if (i <= 0 || chain[i - 1].wagons is null)
            {
                if (i < chain.Length)
                    return InitGetWagon(chain, i + 1);
                return new Wagon[0];
            }
            else
                return chain[i - 1].wagons;
        }

        public static IEnumerable<Locomotive[]>
            GetLocomotivesChains(this IEnumerable<Locomotive> values)
        {
            var ids =
                (from v in values
                 select v.LocomotiveId)
                 .Distinct()
                 .ToArray();

            // temporary data
            IEnumerable<Locomotive> chain;
            IEnumerable<DateTime> dates;

            // cycle
            foreach(var id in ids)
            {
                // get chains of locomotives
                chain =
                    from v in values
                    where id == v.LocomotiveId
                    select v;

                // all the dates of the locomotive
                dates =
                    (from c in chain
                     select c.OperationDate).Distinct();

                // here will be no rows with the same locomotive id and operation date
                yield return
                    (from d in dates
                     select chain.Where(c => c.OperationDate == d).Last())
                    .ToArray();
            }
        }


        public static IEnumerable<Train[]> ConcatTrains(this Train[][] chains)
        {
            var used = new bool[chains.Length];
            // Array.Fill(used, false); // false is default

            var tales =
                from c in chains
                where c.First().OperationId == 10
                select c;

            foreach (var tale in tales)
            {
                var taleIndex = Array.IndexOf(chains, tale);

                if (used[taleIndex])
                    continue;

                var matches =
                    from c in chains
                    where (c.Last().OperationId == 90 || c.Last().OperationId == 12) &&
                    c.Last().TrainId != tale.First().TrainId &&
                    c.Last().RailwayId == tale.First().RailwayId
                    select c;

                if (matches.Count() < 1)
                {
                    yield return tale;

                    used[taleIndex] = true;
                }
                else
                {
                    foreach (var match in matches)
                    {
                        var matchIndex = Array.IndexOf(chains, match);

                        if (used[matchIndex])
                            continue;

                        var firstWithWagons =
                            (from t in tale
                             where wagonOperations.Contains(t.OperationId)
                             select t).FirstOrDefault();

                        var lastWithWagons =
                            (from t in match
                             where wagonOperations.Contains(t.OperationId)
                             select t).LastOrDefault();

                        if (firstWithWagons is null || lastWithWagons is null
                            || firstWithWagons.wagons.Length != lastWithWagons.wagons.Length)
                            continue;

                        var checkWagons = true;

                        foreach (var wagon in lastWithWagons.wagons)
                            if (!firstWithWagons.wagons.Any(w => w.WagonNumber == wagon.WagonNumber))
                            {
                                checkWagons = false;
                                break;
                            }

                        if (checkWagons)
                        {
                            var concated =
                                match.Last().OperationId == 90 ?
                                match.Take(match.Length - 1).Concat(tale).ToArray() :
                                match.Concat(tale).ToArray();

                            yield return concated;

                            used[taleIndex] = true;
                            used[matchIndex] = true;

                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < used.Length; i++)
                if (!used[i])
                    yield return chains[i];
        }

        public static Train[][] GetResult(DateTime begining, DateTime end)
        {
            using (var context = new SqlContext())
            {
                var ids = DbHelper.LoadIds(context, begining, end);
                
                var trains = DbHelper.LoadTrains(context, begining, end, ids);

                var stories = DbHelper.GetStories(trains);

                var wagons = DbHelper.LoadWagons(context, ids);
                DbHelper.SetWagons(stories, wagons);

                var locos = DbHelper.LoadLocomotives(context, ids);
                DbHelper.SetLocomotives(stories, locos);

                stories = stories.ConcatTrains().ToArray();

                return stories;
            }
        }
    }
}