using System.Buffers;
using Craft.Blent.Contracts.Providers;

namespace Craft.Blent.Providers;

public sealed class UniqueIdProvider : IUniqueIdProvider
{
    private const int _length = 13;
    private static long _lastId = DateTime.UtcNow.Ticks;
    private static readonly SpanAction<char, long> _generatelDelegate = GenerateMethod;

    private static void GenerateMethod(Span<char> buffer, long id)
    {
        const string Encode32Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUV";

        buffer[12] = Encode32Chars[(int)id & 31];
        buffer[0] = Encode32Chars[(int)(id >> 60) & 31];
        buffer[1] = Encode32Chars[(int)(id >> 55) & 31];
        buffer[2] = Encode32Chars[(int)(id >> 50) & 31];
        buffer[3] = Encode32Chars[(int)(id >> 45) & 31];
        buffer[4] = Encode32Chars[(int)(id >> 40) & 31];
        buffer[5] = Encode32Chars[(int)(id >> 35) & 31];
        buffer[6] = Encode32Chars[(int)(id >> 30) & 31];
        buffer[7] = Encode32Chars[(int)(id >> 25) & 31];
        buffer[8] = Encode32Chars[(int)(id >> 20) & 31];
        buffer[9] = Encode32Chars[(int)(id >> 15) & 31];
        buffer[10] = Encode32Chars[(int)(id >> 10) & 31];
        buffer[11] = Encode32Chars[(int)(id >> 5) & 31];
    }

    public string Generate()
    {
        var id = Interlocked.Increment(ref _lastId);

        return string.Create(_length, id, _generatelDelegate);
    }
}
