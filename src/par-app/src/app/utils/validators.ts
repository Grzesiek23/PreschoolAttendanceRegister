export function onlyDigits(event: React.KeyboardEvent<HTMLInputElement>) {
    const specialKeys = ['Backspace', 'Delete'];
    if ((event.ctrlKey || event.metaKey) && (event.key === 'a' || event.key === 'x')) { return; }
    if (!/[0-9]/.test(event.key) && !specialKeys.includes(event.key)) {
        event.preventDefault();
    }
}
