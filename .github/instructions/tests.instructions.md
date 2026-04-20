---
description: Standardy testów jednostkowych TaskFlow
applyTo: "**/*Tests.cs, **/Tests/**/*.cs"
---

# Testy jednostkowe – TaskFlow

Używaj xUnit 2, FluentAssertions i NSubstitute. Wzorzec AAA.
Konwencja nazewnictwa: [Metoda]_When[Warunek]_Should[Wynik]

Szablon klasy testowej:
```csharp
public class [Klasa]Tests
{
    private readonly I[Dep] _dep = Substitute.For();
    private readonly [Klasa] _sut;
    public [Klasa]Tests() => _sut = new [Klasa](_dep);
}
```