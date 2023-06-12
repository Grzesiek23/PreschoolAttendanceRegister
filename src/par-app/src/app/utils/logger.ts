export function LogError(error: any): void;
export function LogError(message: string, error: any): void;

export function LogError(messageOrError: string | any, error?: any): void {
    if (import.meta.env.MODE === 'development') {
        if (error) {
            console.error(messageOrError, error);
        } else {
            console.error(messageOrError);
        }
    }
}