using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Monopoly.Scenes.Lands;
using Monopoly.Tools.AssetLoader;
using Monopoly.Tools.Comparers;

namespace Monopoly.Tools.BoardGenerator;

public class BoardBuilder {
    private readonly string _contentPath = "res://Addons/BaseGame/Lands/"; //string.Empty;
    private readonly List<LandBase> _boardContent = [];

    private readonly PropertyAddonLoader _propertyAddonLoader = new();
    private readonly LandAddonLoader _landAddonLoader = new();

    public List<LandBase> BuildBoardContent() {
        LoadContent();

        var positionComparer = new LandBasePositionComparer();
        _boardContent.Sort(positionComparer);

        int x = 0, y = 0;
        var boardDim = _boardContent.Count / 4;

        for (var i = 0; i < _boardContent.Count; i++) {
            var land = _boardContent[i];
            var width = land.Polygon.Max(point => point.X);
            var height = land.Polygon.Max(point => point.Y);

            land.Position = land.Position with { X = x * width, Y = y * height };
            
            if (boardDim * 0 <= i && i < boardDim * 1) {
                x++;
            } else if (boardDim * 1 <= i && i < boardDim * 2) {
                y++;
            } else if (boardDim * 2 <= i && i < boardDim * 3) {
                x--;
            } else if (boardDim * 3 <= i && i < boardDim * 4) {
                y--;
            }
        }

        return _boardContent;
    }

    private void LoadContent() {
        var dirs = DirAccess.Open(_contentPath).GetDirectories();

        foreach (var dir in dirs) {
            var contentFiles = GetFiles(dir);

            foreach (var file in contentFiles) {
                try {
                    _boardContent.Add(_propertyAddonLoader.Load(file));
                }
                catch {
                    _boardContent.Add(_landAddonLoader.Load(file));
                }
            }
        }
    }

    private List<string> GetFiles(string subDir) {
        var directories = DirAccess.Open(_contentPath + $"{subDir}");
        List<string> contentFiles = [];

        contentFiles.AddRange(directories.GetFiles().Select(file => _contentPath + $"{subDir}/{file}"));

        var otherFileIdx = contentFiles.FindIndex(f => !f.EndsWith(".tres"));
        if (otherFileIdx >= 0) contentFiles.RemoveAt(otherFileIdx);

        return contentFiles;
    }
}