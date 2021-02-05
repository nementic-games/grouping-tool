# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2019-07-27
### Added
- Initial release of the tool.

## [1.1.0] - 2019-07-27
### Added
- Support for Unity's shortcut manager
### Changed
- Upgrade from Unity 2018.3 to 2019.1
- Replaced deprecated scene view GUI callback
- Replaced deprecated preference item with new settings provider

## [1.1.1] - 2019-07-28
### Fixed
- There was an issue in which GameObject groups were spawned in a different scene than the selected objects when having multiple scenes open. Now they stay in the scene of their child GameObjects.

## [1.1.2] - 2019-07-29
### Added
- When grouping GameObjects the created group was created at the bottom of the hierarchy. Now the group is created close to the grouped GameObjects preferring the topmost.

## [1.1.3] - 2021-02-05
### Added
- Option for group placement. You can now choose between a centered and origin position.