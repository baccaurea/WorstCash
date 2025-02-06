public class WorstCash
{
    public float _cash = 0;
    public int _format = 0;

    public bool isZero()
    {
        return _cash == 0;
    }
    public bool isOne()
    {
        return _cash == 1 && _format == 0;
    }

    public string Text()
    {
        NormalizeValue();
        if (_cash % 1 == 0)
        {
            return ((int)_cash).ToString() + GetFormat(_format);
        }
        else
        {
            return _cash.ToString("0.##") + GetFormat(_format);
        }
    }
    public void Reset()
    {
        _cash = 0;
        _format = 0;
    }

    public static string GetFormat(int index)
    {
        if (index == 0) return "";
        if (index == 1) return "k";
        if (index == 2) return "m";
        if (index == 3) return "b";
        if (index == 4) return "q";
        if (index == 5) return "t";

        int twoCharTotal = 26 * 26;
        int threeCharTotal = 26 * 26 * 26;
        int fourCharTotal = 26 * 26 * 26 * 26;
        int fiveCharTotal = 26 * 26 * 26 * 26 * 26;
        int sixCharTotal = 26 * 26 * 26 * 26 * 26 * 26;

        index -= 6;

        if (index < twoCharTotal)
        {
            char firstChar = (char)('a' + (index / 26));
            char secondChar = (char)('a' + (index % 26));
            return $"{firstChar}{secondChar}";
        }

        index -= twoCharTotal;
        if (index < threeCharTotal)
        {
            char first = (char)('a' + (index / (26 * 26)));
            int remainder = index % (26 * 26);
            char second = (char)('a' + (remainder / 26));
            char third = (char)('a' + (remainder % 26));
            return $"{first}{second}{third}";
        }

        index -= threeCharTotal;
        if (index < fourCharTotal)
        {
            char first = (char)('a' + (index / (26 * 26 * 26)));
            int remainder = index % (26 * 26 * 26);
            char second = (char)('a' + (remainder / (26 * 26)));
            remainder %= (26 * 26);
            char third = (char)('a' + (remainder / 26));
            char fourth = (char)('a' + (remainder % 26));
            return $"{first}{second}{third}{fourth}";
        }

        index -= fourCharTotal;
        if (index < fiveCharTotal)
        {
            char first = (char)('a' + (index / (26 * 26 * 26 * 26)));
            int remainder = index % (26 * 26 * 26 * 26);
            char second = (char)('a' + (remainder / (26 * 26 * 26)));
            remainder %= (26 * 26 * 26);
            char third = (char)('a' + (remainder / (26 * 26)));
            remainder %= (26 * 26);
            char fourth = (char)('a' + (remainder / 26));
            char fifth = (char)('a' + (remainder % 26));
            return $"{first}{second}{third}{fourth}{fifth}";
        }

        index -= fiveCharTotal;
        if (index < sixCharTotal)
        {
            char first = (char)('a' + (index / (26 * 26 * 26 * 26 * 26)));
            int remainder = index % (26 * 26 * 26 * 26 * 26);
            char second = (char)('a' + (remainder / (26 * 26 * 26 * 26)));
            remainder %= (26 * 26 * 26 * 26);
            char third = (char)('a' + (remainder / (26 * 26 * 26)));
            remainder %= (26 * 26 * 26);
            char fourth = (char)('a' + (remainder / (26 * 26)));
            remainder %= (26 * 26);
            char fifth = (char)('a' + (remainder / 26));
            char sixth = (char)('a' + (remainder % 26));
            return $"{first}{second}{third}{fourth}{fifth}{sixth}";
        }

        return null;
    }

    public static int GetIndex(string sequence)
    {
        if (sequence == "") return 0;
        if (sequence == "k") return 1;
        if (sequence == "m") return 2;
        if (sequence == "b") return 3;
        if (sequence == "q") return 4;
        if (sequence == "t") return 5;

        switch (sequence.Length)
        {
            case 2:
                return 6 + (sequence[0] - 'a') * 26 + (sequence[1] - 'a');

            case 3:
                return 6 + (26 * 26) +
                       (sequence[0] - 'a') * (26 * 26) +
                       (sequence[1] - 'a') * 26 +
                       (sequence[2] - 'a');

            case 4:
                return 6 + (26 * 26 + 26 * 26 * 26) +
                       (sequence[0] - 'a') * (26 * 26 * 26) +
                       (sequence[1] - 'a') * (26 * 26) +
                       (sequence[2] - 'a') * 26 +
                       (sequence[3] - 'a');

            case 5:
                return 6 + (26 * 26 + 26 * 26 * 26 + 26 * 26 * 26 * 26) +
                       (sequence[0] - 'a') * (26 * 26 * 26 * 26) +
                       (sequence[1] - 'a') * (26 * 26 * 26) +
                       (sequence[2] - 'a') * (26 * 26) +
                       (sequence[3] - 'a') * 26 +
                       (sequence[4] - 'a');

            case 6:
                return 6 + (26 * 26 + 26 * 26 * 26 + 26 * 26 * 26 * 26 + 26 * 26 * 26 * 26 * 26) +
                       (sequence[0] - 'a') * (26 * 26 * 26 * 26 * 26) +
                       (sequence[1] - 'a') * (26 * 26 * 26 * 26) +
                       (sequence[2] - 'a') * (26 * 26 * 26) +
                       (sequence[3] - 'a') * (26 * 26) +
                       (sequence[4] - 'a') * 26 +
                       (sequence[5] - 'a');

            default:
                return -1;
        }
    }

    // this worst method
    public void NormalizeValue()
    {
        while (_cash >= 1000f)
        {
            _cash /= 1000f;
            _format++;
        }


        while (_cash < 1f && _format > 0)
        {
            _cash *= 1000f;
            _format--;

        }
    }

    // this worst method
    public float ConvertCash(float value, int fromFormat, int toFormat)
    {
        if (_cash == 0)
        {
            _format = fromFormat;
            return value;
        }
        int difference = toFormat - fromFormat;

        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                value /= 1000f;
            }
        }
        else if (difference < 0)
        {
            for (int i = 0; i < -difference; i++)
            {
                value *= 1000f;
            }
        }

        return value;
    }


    public bool FormatLessthan(string v2)
    {
        return _format < GetIndex(v2);
    }

    public void Add(float value, string format)
    {
        int targetFormat = GetIndex(format);
        float convertedValue = ConvertCash(value, targetFormat, _format);
        _cash += convertedValue;
    }

    public void Substract(float value, string format)
    {
        int targetFormat = GetIndex(format);
        float convertedValue = ConvertCash(value, targetFormat, _format);
        _cash -= convertedValue;
    }

    public void Multiple(float value, string format)
    {
        int targetFormat = GetIndex(format);
        float convertedValue = ConvertCash(value, targetFormat, _format);
        _cash *= convertedValue;

    }

    public void Divide(float value, string format)
    {
        int targetFormat = GetIndex(format);
        float convertedValue = ConvertCash(value, targetFormat, _format);

        if (convertedValue != 0)
        {
            _cash /= convertedValue;
        }
        else
        {
            Debug.LogWarning("Division by zero is not allowed.");
        }
    }

}
