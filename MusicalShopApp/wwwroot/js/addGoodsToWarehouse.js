function observeKindOfGoodsChangeAndChangeSpecificData() {
    let radioButtons = document.getElementsByClassName('new-goods-kind-radio');
    for (let i = 0; i < radioButtons.length; i++) {
        radioButtons[i].addEventListener('change', function () {
            let specificDataDiv = document.getElementById('specific-data');
            removeChildren(specificDataDiv);
            if (this.value == 'MusicalInstruments') {
                specificDataDiv.appendChild(configureReleaseYear());
                // manufacturer type
                let manufacturerTypeId = 'new-goods-manufacturer-type';

                let manufactruerTypeParagraph = document.createElement('p');
                let manufacturerTypeLabel = document.createElement('label');
                manufacturerTypeLabel.setAttribute('for', manufacturerTypeId);
                manufacturerTypeLabel.innerHTML = 'Тип производителя';
                manufactruerTypeParagraph.appendChild(manufacturerTypeLabel);

                let manufacturerTypeRadioGroup = document.createElement('span');
                manufacturerTypeRadioGroup.setAttribute('id', manufacturerTypeId);

                let manufacturerTypeFactoryId = manufacturerTypeId + '-factory';
                let manufacturerTypeFactory = document.createElement('input');
                configureInput(manufacturerTypeFactory, manufacturerTypeFactoryId, 'radio', 'ManufacturerType');
                manufacturerTypeFactory.setAttribute('value', 'Factory');
                manufacturerTypeFactory.setAttribute('checked', 'checked');
                manufacturerTypeRadioGroup.appendChild(manufacturerTypeFactory);

                let manufacturerTypeFactoryLabel = document.createElement('label');
                manufacturerTypeFactoryLabel.setAttribute('for', manufacturerTypeFactoryId);
                manufacturerTypeFactoryLabel.innerHTML = 'Завод';
                manufacturerTypeRadioGroup.appendChild(manufacturerTypeFactoryLabel);

                let manufacturerTypeMasterId = manufacturerTypeId + '-master';
                let manufacturerTypeMaster = document.createElement('input');
                configureInput(manufacturerTypeMaster, manufacturerTypeMasterId, 'radio', 'ManufacturerType');
                manufacturerTypeMaster.setAttribute('value', 'Master');
                manufacturerTypeRadioGroup.appendChild(manufacturerTypeMaster);

                let manufacturerTypeMasterLabel = document.createElement('label');
                manufacturerTypeMasterLabel.setAttribute('for', manufacturerTypeMasterId);
                manufacturerTypeMasterLabel.innerHTML = 'Мастер';
                manufacturerTypeRadioGroup.appendChild(manufacturerTypeMasterLabel);
                manufactruerTypeParagraph.appendChild(manufacturerTypeRadioGroup);

                specificDataDiv.appendChild(manufactruerTypeParagraph);
                // manufacturer name
                let manufacturerNameParagraph = document.createElement('p');
                let manufacturerNameLabel = document.createElement('label');
                let manufacturerNameId = 'new-goods-manufacturer-name';
                manufacturerNameLabel.setAttribute('for', manufacturerNameId);
                manufacturerNameLabel.innerHTML = 'Название производителя';
                manufacturerNameParagraph.appendChild(manufacturerNameLabel);

                let manufacturerNameInput = document.createElement('input');
                configureInput(manufacturerNameInput, manufacturerNameId, 'text', 'Manufacturer');
                manufacturerNameParagraph.appendChild(manufacturerNameInput);

                specificDataDiv.appendChild(manufacturerNameParagraph);
            }
            else if (this.value == 'Accessories') {
                removeChildren(specificDataDiv);
                // color div
                let colorDiv = document.createElement('p');

                let colorLabel = document.createElement('label');
                let colorId = 'new-goods-color';
                colorLabel.setAttribute('for', colorId);
                colorLabel.innerHTML = 'Цвет';
                colorDiv.appendChild(colorLabel);

                let colorInput = document.createElement('input');
                configureInput(colorInput, colorId, 'text', 'Color')
                colorDiv.appendChild(colorInput);

                specificDataDiv.appendChild(colorDiv);
                //size div
                let sizeParagraph = document.createElement('p');

                let sizeLabel = document.createElement('label');
                let sizeId = 'new-goods-size';
                sizeLabel.setAttribute('for', sizeId);
                sizeLabel.innerHTML = 'Размер';
                sizeParagraph.appendChild(sizeLabel);

                let sizeInput = document.createElement('input');
                configureInput(sizeInput, sizeId, 'text', 'Size')
                sizeParagraph.appendChild(sizeInput);

                specificDataDiv.appendChild(sizeParagraph);
            }
            else if (this.value == 'AudioEquipmentUnits') {
                removeChildren(specificDataDiv);
            }
            else if (this.value == 'SheetMusicEditions') {
                removeChildren(specificDataDiv);
                // author
                let authorParagraph = document.createElement('p');

                let authorLabel = document.createElement('label');
                let authorId = 'new-goods-author';
                authorLabel.setAttribute('for', authorId);
                authorLabel.innerHTML = 'Автор';
                authorParagraph.appendChild(authorLabel);

                let authorInput = document.createElement('input');
                configureInput(authorInput, authorId, 'text', 'Author')
                authorParagraph.appendChild(authorInput);

                specificDataDiv.appendChild(configureReleaseYear());
                specificDataDiv.appendChild(authorParagraph);
            }
        });
    };
}

function observeKindOfGoodsChangeAndSwitchSpecificTypesGroup() {
    let radioButtons = document.getElementsByClassName('new-goods-kind-radio');
    for (let i = 0; i < radioButtons.length; i++) {
        radioButtons[i].addEventListener('change', function () {
            let select = document.getElementById('AddGoodsToWarehouseDto_SpecificType');
            if (this.value == 'MusicalInstruments') {
                disableAllGroupsExcept('Музыкальные инструменты');
            }
            else if (this.value == 'Accessories') {
                disableAllGroupsExcept('Аксессуары');
            }
            else if (this.value == 'AudioEquipmentUnits') {
                disableAllGroupsExcept('Аудиооборудование');
            }
            else if (this.value == 'SheetMusicEditions') {
                disableAllGroupsExcept('Нотные издания');
            }
            function disableAllGroupsExcept(optgroupToNotDisable) {
                let optgroupToEnable = select.querySelector('[label="' + optgroupToNotDisable + '"]');
                if (optgroupToEnable != null) {
                    optgroupToEnable.removeAttribute('disabled');
                    // TODO check checkbox when there're 0 specific types
                    let options = select.getElementsByTagName('option');
                    for (var i = 0; i < options.length; i++) {
                        if (options[i].value == optgroupToEnable.children[0].value) {
                            options[i].selected = true;
                            break;
                        }
                    }
                }
                else {
                    // TODO
                }
                Array.from(select.children).forEach(function (anotherOptgroup) {
                    if (anotherOptgroup != optgroupToEnable) {
                        anotherOptgroup.setAttribute('disabled', 'disabled');
                    }
                });
            }
        });
    }
    
}

function observeNewSpecificTypeCheckboxChecked() {
    $('#AddGoodsToWarehouseDto_NewSpecificTypeIsBeingAdded')[0].addEventListener('change', function () {
        let specificTypesSelect = $('#AddGoodsToWarehouseDto_SpecificType')[0];
        let newSpecificTypeInput = $('#AddGoodsToWarehouseDto_NewSpecificType')[0];
        if (this.checked) {
            specificTypesSelect.setAttribute('disabled', 'disabled');
            newSpecificTypeInput.removeAttribute('disabled');
        }
        else {
            newSpecificTypeInput.setAttribute('disabled', 'disabled');
            specificTypesSelect.removeAttribute('disabled');
            newSpecificTypeInput.value = '';
        }
    });
}


// TODO does it work?
//$(document).ready(displaySpecificData, replaceSpecificTypes);
$(document).ready(function () {
    observeKindOfGoodsChangeAndChangeSpecificData();
    observeKindOfGoodsChangeAndSwitchSpecificTypesGroup();
    observeNewSpecificTypeCheckboxChecked();
});

function removeChildren(element) {
    element.innerHTML = '';
}

function configureInput(input, id, type, name) {
    input.setAttribute('id', id);
    input.setAttribute('type', type);
    input.setAttribute('name', specificData(name));
}

function specificData(name) {
    return 'AddGoodsToWarehouseDto.GoodsKindSpecificDataDto.' + name;
}

function configureReleaseYear() {
    let releaseYearId = 'new-goods-release-year';
    let releaseYearParagraph = document.createElement('p');

    let releaseYearLabel = document.createElement('label');
    releaseYearLabel.setAttribute('for', releaseYearId);
    releaseYearLabel.innerHTML = 'Год выпуска';
    releaseYearParagraph.appendChild(releaseYearLabel);

    let releaseYear = document.createElement('input');
    configureInput(releaseYear, releaseYearId, 'number', 'ReleaseYear');
    releaseYearParagraph.appendChild(releaseYear);
    return releaseYearParagraph;
}