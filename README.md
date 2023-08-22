# TrickGod
Mod for Bombrush Cyberfunk that makes getting high scores extremely easy
![Screenshot of player using trick god to score over 99 million points with just 21 tricks](/Screenshot_TrickGod.png)


## Features
### Version 1.0.0
- Trick God Toggle w/ indicator
- Adjustable Boost Increments [0 - 1000]

## Installation
- Download [BepInEx 5.4.21](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21) and extract it to your BRCF install directory.
- Open Bomb Rush Cyberfunk so BepInEx will run its setup, then close the game once you're at the main menu.
- Download TrickGod.dll from GitHub and place it into `[BRCF Install Dir]\BepInEx\plugins`
- Press `'` (single quote) in-game to toggle the menu

## Building from source
- Clone this repository
- Run dotnet build
- Copy `bin\debug\net47\TrickGod.dll` to `[BRCF Install Dir]\BepInEx\plugins`

# Keybinds
- `= or keypad +` : Increase Boost Amount [+5]
- `- or keypad -` : Decrease Boost Amount [-5]
- `\` : Toggle Trick God Mode
- `'` : Toggle UI

