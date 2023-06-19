export function onlyDigits(event: React.KeyboardEvent<HTMLInputElement>) {
    if (!/[0-9]/.test(event.key)) {
        event.preventDefault();
    }
}