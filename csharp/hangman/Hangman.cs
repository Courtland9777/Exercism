using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

public class HangmanState
{
    public HangmanState(string maskedWord, ImmutableHashSet<char> guessedChars, int remainingGuesses)
    {
        MaskedWord = maskedWord;
        GuessedChars = guessedChars;
        RemainingGuesses = remainingGuesses;
    }

    public string MaskedWord { get; }
    public ImmutableHashSet<char> GuessedChars { get; }
    public int RemainingGuesses { get; }
}

public class TooManyGuessesException : Exception
{
}

public class Hangman : ISubject<char, HangmanState>
{
    private bool _finished;
    private bool _tooManyGuess;

    public Hangman(string word)
    {
        Word = word;
        Observers = new List<IObserver<HangmanState>>();
        State = new HangmanState(new string('_', Word.Length), ImmutableHashSet<char>.Empty, 9);
    }

    public IObservable<HangmanState> StateObservable => this;
    public IObserver<char> GuessObserver => this;
    private string Word { get; }
    private HangmanState State { get; set; }
    private List<IObserver<HangmanState>> Observers { get; }

    public void OnCompleted() { }
    public void OnError(Exception error) { }

    public void OnNext(char value)
    {
        var unmasked = IsMatched(value) ? Unmask(value) : State.MaskedWord;
        _finished = unmasked == Word;
        _tooManyGuess = State.RemainingGuesses - 1 < 0 && !IsMatched(value);
        State = new HangmanState(
            unmasked,
            State.GuessedChars.Add(value),
            IsMatched(value) ? State.RemainingGuesses : State.RemainingGuesses - 1
        );
        Observers.ForEach(Validate);
    }

    public IDisposable Subscribe(IObserver<HangmanState> observer)
    {
        Validate(observer);
        Observers.Add(observer);
        return Disposable.Empty;
    }

    private void Validate(IObserver<HangmanState> observer)
    {
        switch (true)
        {
            case var b when _finished:
                observer.OnCompleted();
                break;
            case var b when _tooManyGuess:
                observer.OnError(new TooManyGuessesException());
                break;
            default:
                observer.OnNext(State);
                break;
        }
    }

    private bool IsMatched(char value) => Word.Contains(value) && !State.GuessedChars.Contains(value);
    private string Unmask(char value) => new(Word.Select((c, i) => c == value ? c : State.MaskedWord[i]).ToArray());
}