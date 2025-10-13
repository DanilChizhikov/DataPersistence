# DataPersistence

## Status
![](https://img.shields.io/badge/unity-2022.3+-000.svg)
![Tests](https://github.com/DanilChizhikov/DataPersistence/actions/workflows/unity-tests.yml/badge.svg)

## Description

## Table of Contents
- [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Manual Installation](#manual-installation)
    - [UPM Installation](#upm-installation)
- [Basic Usage](#basic-usage)
- [Testing](#testing)
- [License](#license)
- 
## Getting Started
### Prerequisites
- [GIT](https://git-scm.com/downloads)
- [Unity](https://unity.com/releases/editor/archive) 2022.3+

### Manual Installation
1. Download the .unitypackage from the [releases](https://github.com/DanilChizhikov/DataPersistence/releases/) page.
2. Import com.dtech.data-persistence.x.x.x.unitypackage into your project.

### UPM Installation
1. Open the manifest.json file in your project's Packages folder.
2. Add the following line to the dependencies section:
    ```json
    "com.dtech.data-persistence": "https://github.com/DanilChizhikov/DataPersistence.git",
    ```
3. Unity will automatically import the package.

If you want to set a target version, DataPersistence uses the `v*.*.*` release tag so you can specify a version like #v0.1.0.

For example `https://github.com/DanilChizhikov/DataPersistence.git#v0.1.0`.

## Testing
The project includes a comprehensive set of unit tests that verify the correct operation of the object pool. All tests are passing successfully.

To run the tests:
1. Open the Test Runner window (Window > General > Test Runner)
2. Select PlayMode tests
3. Click "Run All"

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.